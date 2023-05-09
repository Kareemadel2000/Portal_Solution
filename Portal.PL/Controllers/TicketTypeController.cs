using Microsoft.AspNetCore.Mvc;
using Portal.Domains.Entities;
using MediatR;
using Portal.Persistence.DataBase;
using Microsoft.AspNetCore.Authorization;
using Portal.Domains.Enums;
using Microsoft.AspNetCore.Authentication.Cookies;
using Portal.Application.Errors_Handler_Helper.Features.TicketType.Commands.CreateTicketType;
using Portal.Application.Errors_Handler_Helper.Features.TicketType.Commands.UpdateTicketType;
using Portal.Application.Errors_Handler_Helper.Features.TicketType.Queries.GetAllTicketTypes;
using Portal.Application.Errors_Handler_Helper.Features.TicketType.Queries.GetTiketTypeById;
using Portal.Application.Errors_Handler_Helper.Features.TicketType.Commands.DeleteTicketType;

namespace Portal.PL.Controllers
{
    [Authorize]
    public class TicketTypeController : BaseController
    {
      
        private readonly IMediator _mediator;

        public TicketTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult<IReadOnlyList<GetAllTicketTypesResponseDto>>> Index()
        {
            var result = await _mediator.Send(new GetAllTicketTypesQuery());
            ViewBag.data = result;
            return View();
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GetAllTicketTypesResponseDto>>> GetAllTicketTypes()
        {
            var result = await _mediator.Send(new GetAllTicketTypesQuery());
            return Ok(result);

        }
        [HttpPost]
        public async Task<ActionResult<GetAllTicketTypesResponseDto>> Create([FromBody] CreateTicketTypeCommand command)
        {
            var result = await _mediator.Send(command);
            return Json(Ok(result));
        }        
        public async Task<ActionResult<GetAllTicketTypesResponseDto>> Ticket(int id)
        {
            var result = await _mediator.Send(new GetTicketTypByIdQuery { Id = id});
            return Json(Ok(result));
        }

        [HttpPut]
        public async Task<ActionResult<UpdateTicketTypeCommandResponseDto>> Update([FromBody] UpdateTicketTypeCommand command)
        {

            var result = await _mediator.Send(command);
                return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteTicketTypeCommand { Id = id });
            return Ok(result);
        }
    }
}
