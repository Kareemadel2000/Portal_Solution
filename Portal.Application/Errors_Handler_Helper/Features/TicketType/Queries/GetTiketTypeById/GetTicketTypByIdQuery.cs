using MediatR;
using Portal.Application.Errors_Handler_Helper.Features.TicketType.Queries.GetAllTicketTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.TicketType.Queries.GetTiketTypeById
{
    public class GetTicketTypByIdQuery : IRequest<GetAllTicketTypesResponseDto>
    {
        public int Id { get; set; }
    }
}
