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

namespace Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketClient_Features.Queries.GetTicketById
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, GeneralTicketQueryResponseDto>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public GetTicketByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<GeneralTicketQueryResponseDto> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = await userManager.GetUserIdAsyncExtension(request.user);
            var spec = new TicketForUserByAccountIdSpecifications(request.Id, userId);
            var ticket = await unitOfWork.Repository<Ticket>().GetByIdWithSpecificationsAsync(spec);
            if (ticket == null)
                throw new ApiErrorResponse(404, "This Ticket Not Found.");
            return mapper.Map<GeneralTicketQueryResponseDto>(ticket);
        }
    }
}
