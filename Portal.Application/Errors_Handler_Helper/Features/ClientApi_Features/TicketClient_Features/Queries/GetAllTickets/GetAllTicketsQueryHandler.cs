using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Portal.Application.Contracts;
using Portal.Application.Helper.Extensions;
using Portal.Application.Specifications.Ticket_Specifications;
using Portal.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketClient_Features.Queries.GetAllTickets
{
    public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, IReadOnlyList<GeneralTicketQueryResponseDto>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public GetAllTicketsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IReadOnlyList<GeneralTicketQueryResponseDto>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {
            var userId = await userManager.GetUserIdAsyncExtension(request.user);
            var spec = new TicketForUserByAccountIdSpecifications(userId);
            var data = await unitOfWork.Repository<Ticket>().GetAllWithSpecificationsAsync(spec);
            return mapper.Map<IReadOnlyList<GeneralTicketQueryResponseDto>>(data);
        }
    }
}
