using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Application.Contracts;
using Portal.Application.Specifications.Ticket_Specifications;
using Portal.Application.ViewModels;
using Portal.Domains.Entities;
using Portal.Domains.Enums;

namespace Portal.PL.Controllers
{
    [Authorize]
    public class TicketWithAttachmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> userManager;
        public TicketWithAttachmentsController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int? ticketTypeId = null)
        {
            var ticketTypes = await _unitOfWork.Repository<TicketType>().GetAllAsync();            
            ViewBag.Types = ticketTypes;

            var ticketsSpec = ticketTypeId == null ? new TicketForUserByAccountIdSpecifications() : new TicketForUserByAccountIdSpecifications(ticketTypeId.Value);
            var tickets = await _unitOfWork.Repository<Ticket>().GetAllWithSpecificationsAsync(ticketsSpec);

            List<TicketWithAttachmentViewModel>? model = new List<TicketWithAttachmentViewModel>();
            foreach (var item in tickets)
            {
                var user = await userManager.FindByIdAsync(item.UserId);
                var ticketvm = new TicketWithAttachmentViewModel()
                {
                    Id = item.Id,
                    Status = item.Status,
                    Description = item.Description,
                    Email = user.Email,
                    TicketTypeId = item.TicketTypeId,
                    Title = item.Title,
                    UserId = item.UserId,
                    UserName = user.UserName,
                    AttachmentImageUrl = item.TicketAttachments?.Select(x => x.ImageUrl).ToList()!
                };
                model.Add(ticketvm);
            }            
            return View(model);
        }
    }
}
