using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.TicketType.Commands.DeleteTicketType
{
    public class DeleteTicketTypeCommand : IRequest<DeleteTicketTypeCommandResponseDto>
    {
        public int Id { get; set; }

    }
}
