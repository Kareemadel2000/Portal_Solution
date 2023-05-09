using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketClient_Features.Commands.CreateTicket
{
    public class CreateTicketRequestDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int TicketTypeId { get; set; }
    }
}
