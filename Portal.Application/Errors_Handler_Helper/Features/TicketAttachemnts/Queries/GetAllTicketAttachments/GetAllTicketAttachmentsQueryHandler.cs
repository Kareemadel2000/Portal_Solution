using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Portal.Application.Contracts;
using Portal.Application.Errors_Handler_Helper.Custom_Exceptions;
using Portal.Application.Specifications.Attachment_Specifications;
using Portal.Application.Specifications.Ticket_Specifications;
using Portal.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.TicketAttachemnts.Queries.GetAllTicketAttachments
{
    public class GetAllTicketAttachmentsQueryHandler : IRequestHandler<GetAllTicketAttachmentsQuery, IReadOnlyList<GetAllTicketAttachmentsResponseDto>>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public GetAllTicketAttachmentsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<IReadOnlyList<GetAllTicketAttachmentsResponseDto>> Handle(GetAllTicketAttachmentsQuery request, CancellationToken cancellationToken)
        {
            var email = request.Claims.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new ApiErrorResponse(404, "Client Not Found.");

            var tickectSpec = new TicketForUserByAccountIdSpecifications(request.TicketId, user.Id);
            var tickect = await _unitOfWork.Repository<Ticket>().GetByIdWithSpecificationsAsync(tickectSpec);
            if (tickect == null)
                throw new ApiErrorResponse(404, "Ticket Not Exist For This User.");

            var GetAllTicketAttachmnetSpec = new GetAllAttachmentsForTicketAndUserSpecifications(tickect.Id);
            var GetAllTicketAttachmnet = await _unitOfWork.Repository<TicketAttachment>().GetAllWithSpecificationsAsync(GetAllTicketAttachmnetSpec);
            return _mapper.Map<IReadOnlyList<GetAllTicketAttachmentsResponseDto>>(GetAllTicketAttachmnet);
        }
    }
}
