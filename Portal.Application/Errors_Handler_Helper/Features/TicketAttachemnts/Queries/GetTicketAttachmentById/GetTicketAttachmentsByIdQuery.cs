using MediatR;
using Portal.Application.Errors_Handler_Helper.Features.TicketAttachemnts.Queries.GetAllTicketAttachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.TicketAttachemnts.Queries.GetTicketAttachmentById
{
    public class GetTicketAttachmentsByIdQuery : IRequest<GetAllTicketAttachmentsResponseDto>
    {
        public ClaimsPrincipal Claims { get; set; }
        public int TicketId { get; set; }
        public int Id { get; set; }
    }
}
