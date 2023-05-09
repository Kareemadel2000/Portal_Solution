using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketClient_Features.Commands.CreateTicket
{
    public class CreateTicketCommandValidators : AbstractValidator<CreateTicketCommand>
    {
        public CreateTicketCommandValidators()
        {
            RuleFor(x => x.Ticket.Title).NotEmpty().NotNull().MaximumLength(100);
            RuleFor(x => x.Ticket.Description).NotEmpty().NotNull();
            RuleFor(x => x.Ticket.TicketTypeId).NotEmpty().NotNull();
        }
    }
}
