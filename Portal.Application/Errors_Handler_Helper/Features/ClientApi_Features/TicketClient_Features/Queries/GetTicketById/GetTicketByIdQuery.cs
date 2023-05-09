using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketClient_Features.Queries.GetTicketById
{
    public class GetTicketByIdQuery : IRequest<GeneralTicketQueryResponseDto>
    {
        public int Id { get; set; }
        public ClaimsPrincipal user { get; set; }
    }
}
