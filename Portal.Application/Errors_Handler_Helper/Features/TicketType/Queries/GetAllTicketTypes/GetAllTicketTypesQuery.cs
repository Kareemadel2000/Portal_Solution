using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.TicketType.Queries.GetAllTicketTypes
{
    public class GetAllTicketTypesQuery : IRequest<IReadOnlyList<GetAllTicketTypesResponseDto>>
    {
    }
}
