using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.TicketType.Commands.UpdateTicketType
{
    public class UpdateTicketTypeCommand : IRequest<UpdateTicketTypeCommandResponseDto>
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
    }
}
