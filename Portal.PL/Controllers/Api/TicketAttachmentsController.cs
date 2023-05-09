using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Errors_Handler_Helper.Features.TicketAttachemnts.Commends.CreateTicketAttachemnts;
using Portal.Application.Errors_Handler_Helper.Features.TicketAttachemnts.Queries.GetAllTicketAttachments;
using Portal.Application.Errors_Handler_Helper.Features.TicketAttachemnts.Queries.GetTicketAttachmentById;

namespace Portal.PL.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="Client")]
    public class TicketAttachmentsController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<CreateTicketAttachmentTypeResponseDto>> CreateTicketAttachment([FromForm] RequestDto request)
        {
            var result = await _mediator.Send(new CreateTicketAttachemuntsCommand
            {                
                Claims = User,
                attachmentRequestDto = new CreateTicketAttachmentRequestDto()
                {
                    File = request.File,
                    TicketId = request.TicketId,
                    Id = 0
                }
            });
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<CreateTicketAttachmentTypeResponseDto>> UpdateTicketAttachment([FromForm] CreateTicketAttachmentRequestDto request)
        {
            var result = await _mediator.Send(new CreateTicketAttachemuntsCommand
            {
                attachmentRequestDto = request,
                Claims = User                
            });
            return Ok(result);
        }
        [HttpGet("{ticketId}")]
        public async Task<ActionResult<IReadOnlyList<GetAllTicketAttachmentsResponseDto>>> GetAllAttachments(int ticketId)
        {
            var response = await _mediator.Send(new GetAllTicketAttachmentsQuery { Claims = User, TicketId = ticketId });
            return Ok(response);
        }
        [HttpGet("{id}/ticket/{ticketId}")]
        public async Task<ActionResult<GetAllTicketAttachmentsResponseDto>> GetAllAttachments(int id, int ticketId)
        {
            var response = await _mediator.Send(new GetTicketAttachmentsByIdQuery { Claims = User, Id = id, TicketId = ticketId });
            return Ok(response);
        }
    }
}
