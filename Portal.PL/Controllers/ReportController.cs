using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portal.Domains.Entities;

namespace Portal.PL.Controllers
{
    public class ReportController : Controller
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public IActionResult TicketsByPeriod()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> TicketsByPeriod(DateTime startDate, DateTime endDate)
        {
          
            
            var tickets = await _mediator.Send(new GetTicketsByPeriodQuery { StartDate = startDate, EndDate = endDate });
            return View("TicketsByPeriodResult", tickets);
        }


        public class GetTicketsByPeriodQuery : IRequest<List<Ticket>>
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }
    }
}
