using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketClient_Features.Commands.UpdateTicket
{
    public class UpdateTicketCommandValidators : AbstractValidator<UpdateTicketRequestDto>
    {
        public UpdateTicketCommandValidators()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Title).NotEmpty().NotNull();
            RuleFor(x => x.TicketTypeId).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
        }
    }
}
