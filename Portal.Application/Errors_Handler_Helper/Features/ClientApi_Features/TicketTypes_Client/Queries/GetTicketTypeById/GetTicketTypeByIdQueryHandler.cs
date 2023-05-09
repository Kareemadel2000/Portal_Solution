using AutoMapper;
using MediatR;
using Portal.Application.Contracts;
using Portal.Application.Errors_Handler_Helper.Custom_Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketTypes_Client.Queries.GetTicketTypeById
{
    public class GetTicketTypeByIdQueryHandler : IRequestHandler<GetTicketTypeByIdQuery, GenericTicketTypeResponseDto>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetTicketTypeByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<GenericTicketTypeResponseDto> Handle(GetTicketTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var ticketType = await unitOfWork.Repository<Domains.Entities.TicketType>().GetByIdAsync(request.Id);
            if (ticketType == null)
                throw new ApiErrorResponse(404, "This Type Does'nt Exist.");
            return mapper.Map<GenericTicketTypeResponseDto>(ticketType);
        }
    }
}
