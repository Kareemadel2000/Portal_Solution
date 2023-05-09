using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Portal.Application.Contracts;
using Portal.Application.Errors_Handler_Helper.Custom_Exceptions;
using Portal.Application.Helper.Extensions;
using Portal.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketClient_Features.Commands.CreateTicket
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, GeneralTicketCommandResponseDto>
    {
        private readonly IUnitOfWork unitOfWork;
        private UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;

        public CreateTicketCommandHandler(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<GeneralTicketCommandResponseDto> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var type = await unitOfWork.Repository<Portal.Domains.Entities.TicketType>().GetByIdAsync(request.Ticket.TicketTypeId);
            if (type == null)
                throw new ApiErrorResponse(404, "Type Not Exist.");

            var ticket = mapper.Map<Ticket>(request.Ticket);
            ticket.UserId = await userManager.GetUserIdAsyncExtension(request.User);

            await unitOfWork.Repository<Ticket>().AddAsync(ticket);

            if (await unitOfWork.Complete() <= 0)
                throw new ApiErrorResponse(400, "Try Again.");

            return new GeneralTicketCommandResponseDto()
            {
                IsSuccess = true,
                Message = "Ticket Added Successfully."
            };
        }
    }
}
