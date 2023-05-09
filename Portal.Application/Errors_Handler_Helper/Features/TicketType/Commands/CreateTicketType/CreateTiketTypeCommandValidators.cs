using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.TicketType.Commands.CreateTicketType
{
    public class CreateTiketTypeCommandValidators : AbstractValidator<CreateTicketTypeCommand>
    {
        public CreateTiketTypeCommandValidators()
        {
            RuleFor(x => x.TypeName).NotNull().NotEmpty();
        }
    }
}
