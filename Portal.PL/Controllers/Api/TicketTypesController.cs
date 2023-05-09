using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Errors_Handler_Helper.Custom_Exceptions;
using Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketTypes_Client.Queries;
using Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketTypes_Client.Queries.GetAllTickeTypes;
using Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketTypes_Client.Queries.GetTicketTypeById;
using Portal.Domains.Enums;

namespace Portal.PL.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Client")]
    public class TicketTypesController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        public async Task<ActionResult<IReadOnlyList<GenericTicketTypeResponseDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllTicketTypeQueary());
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        public async Task<ActionResult<GenericTicketTypeResponseDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetTicketTypeByIdQuery() { Id = id });
            return Ok(result);
        }
    }
}
