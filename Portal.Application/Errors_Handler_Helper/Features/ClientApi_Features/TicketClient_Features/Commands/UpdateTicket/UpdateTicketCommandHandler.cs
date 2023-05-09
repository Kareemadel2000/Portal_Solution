using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Portal.Application.Contracts;
using Portal.Application.Errors_Handler_Helper.Custom_Exceptions;
using Portal.Application.Helper.Extensions;
using Portal.Application.Specifications.Ticket_Specifications;
using Portal.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketClient_Features.Commands.UpdateTicket
{
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, GeneralTicketCommandResponseDto>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;

        public UpdateTicketCommandHandler(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<GeneralTicketCommandResponseDto> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            var userId = await userManager.GetUserIdAsyncExtension(request.User);
            var spec = new TicketForUserByAccountIdSpecifications(request.Ticket.Id, userId);
            var ticket = await unitOfWork.Repository<Ticket>().GetByIdWithSpecificationsAsync(spec);
            if (ticket == null)
                throw new ApiErrorResponse(404, "Ticket Not Exist.");
            if (await unitOfWork.Repository<Portal.Domains.Entities.TicketType>().GetByIdAsync(request.Ticket.TicketTypeId) == null)
                throw new ApiErrorResponse(404, "Type Not Exist.");
            ticket = mapper.Map(request.Ticket, ticket);
            unitOfWork.Repository<Ticket>().Update(ticket);
            if (await unitOfWork.Complete() <= 0)
                throw new ApiErrorResponse(400, null);
            return new GeneralTicketCommandResponseDto
            {
                IsSuccess = true,
                Message = "Ticket Updated Successfully."
            };
        }
    }
}
