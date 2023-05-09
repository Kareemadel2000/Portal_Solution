using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Portal.Application.Contracts;
using Portal.Application.Errors_Handler_Helper.Custom_Exceptions;
using Portal.Application.Errors_Handler_Helper.Features.TicketAttachemnts.Queries.GetAllTicketAttachments;
using Portal.Application.Specifications.Attachment_Specifications;
using Portal.Application.Specifications.Ticket_Specifications;
using Portal.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.TicketAttachemnts.Queries.GetTicketAttachmentById
{
    public class GetTicketAttachmentsByIdQueryHeandler : IRequestHandler<GetTicketAttachmentsByIdQuery, GetAllTicketAttachmentsResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public GetTicketAttachmentsByIdQueryHeandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<GetAllTicketAttachmentsResponseDto> Handle(GetTicketAttachmentsByIdQuery request, CancellationToken cancellationToken)
        {
            var email = request.Claims.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new ApiErrorResponse(404, "Client Not Found.");
            }

            var tickectSpecById = new TicketForUserByAccountIdSpecifications(request.TicketId, user.Id);
            var tickets = await _unitOfWork.Repository<Ticket>().GetByIdWithSpecificationsAsync(tickectSpecById);
            if (tickets == null)
                throw new ApiErrorResponse(404, "Ticket Not Exist For This UserId.");
            var GetAllTicketAttachmnetSpec = new GetAllAttachmentsForTicketAndUserSpecifications(request.Id, tickets.Id);

            var typtAttachmentById = await _unitOfWork.Repository<TicketAttachment>().GetByIdWithSpecificationsAsync(GetAllTicketAttachmnetSpec);
            if (typtAttachmentById == null)
                throw new ApiErrorResponse(404, "Not Exist.");
            return _mapper.Map<GetAllTicketAttachmentsResponseDto>(typtAttachmentById);
        }
    }
}
