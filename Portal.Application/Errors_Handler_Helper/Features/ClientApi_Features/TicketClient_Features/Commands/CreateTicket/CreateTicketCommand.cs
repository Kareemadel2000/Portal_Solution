using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketClient_Features.Commands.CreateTicket
{
    public class CreateTicketCommand : IRequest<GeneralTicketCommandResponseDto>
    {
        public ClaimsPrincipal User { get; set; }
        public CreateTicketRequestDto Ticket { get; set; }
    }
}
