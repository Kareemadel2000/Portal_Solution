using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Domains.Entities;

namespace Portal.PL.Controllers
{
    // TicketController.cs
    public class TicketController : Controller
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Pending()
        {
            var tickets = await _mediator.Send(new GetPendingTicketsQuery());
            return View(tickets);
        }

        [HttpPost]
        [Authorize(Policy = "AcceptRejectTicketPolicy")]
        public async Task<IActionResult> Accept(int ticketId, string reason)
        {
            await _mediator.Send(new AcceptTicketCommand { TicketId = ticketId, Reason = reason });
            return RedirectToAction("Pending");
        }

        [HttpPost]
        [Authorize(Policy = "AcceptRejectTicketPolicy")]
        public async Task<IActionResult> Reject(int ticketId, string reason)
        {
            await _mediator.Send(new RejectTicketCommand { TicketId = ticketId, Reason = reason });
            return RedirectToAction("Pending");
        }

        
    }

    // GetPendingTicketsQuery
    public class GetPendingTicketsQuery : IRequest<List<Ticket>> { }

    // AcceptTicketCommand
    public class AcceptTicketCommand : IRequest
    {
        public int TicketId { get; set; }
        public string Reason { get; set; }
    }

    // RejectTicketCommand
    public class RejectTicketCommand : IRequest
    {
        public int TicketId { get; set; }
        public string Reason { get; set; }
    }

}
