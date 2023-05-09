using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.TicketAttachemnts.Commends.CreateTicketAttachemnts
{
    public class CreateTicketAttachemuntsCommand : IRequest<CreateTicketAttachmentTypeResponseDto>
    {        
        public ClaimsPrincipal Claims { get; set; }
        public CreateTicketAttachmentRequestDto attachmentRequestDto { get; set; }

    }
}
