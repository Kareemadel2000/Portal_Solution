using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.TicketType.Commands.UpdateTicketType
{
    public class UpdateTicketTypeCommandValidators : AbstractValidator<UpdateTicketTypeCommand>
    {
        public UpdateTicketTypeCommandValidators()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.TypeName).NotEmpty().NotNull();
        }
    }
}
