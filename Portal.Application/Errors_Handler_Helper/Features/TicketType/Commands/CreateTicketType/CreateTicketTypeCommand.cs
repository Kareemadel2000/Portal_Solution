
using MediatR;
using Portal.Application.Errors_Handler_Helper.Features.TicketType.Queries.GetAllTicketTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.TicketType.Commands.CreateTicketType
{
    public class CreateTicketTypeCommand : IRequest<GetAllTicketTypesResponseDto>
    {
        public string? TypeName { get; set; }
    }
}
