using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketClient_Features.Commands;
using Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketClient_Features.Commands.CreateTicket;
using Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketClient_Features.Commands.UpdateTicket;
using Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketClient_Features.Queries;
using Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketClient_Features.Queries.GetAllTickets;
using Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketClient_Features.Queries.GetTicketById;

namespace Portal.PL.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Client")]
    public class TicketsController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<GeneralTicketCommandResponseDto>> CreateTicket([FromBody] CreateTicketRequestDto create)
        {
            var result = await _mediator.Send(new CreateTicketCommand { User = this.User, Ticket = create });
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<GeneralTicketCommandResponseDto>> UpdateTicket([FromBody] UpdateTicketRequestDto update)
        {
            var result = await _mediator.Send(new UpdateTicketCommand { User = this.User, Ticket = update });
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GeneralTicketQueryResponseDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllTicketsQuery { user = this.User });
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralTicketQueryResponseDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetTicketByIdQuery { Id = id, user = this.User });
            return Ok(result);
        }
    }
}
