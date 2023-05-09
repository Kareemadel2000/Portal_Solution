using AutoMapper;
using MediatR;
using Portal.Application.Contracts;
using Portal.Application.Errors_Handler_Helper.Custom_Exceptions;
using Portal.Application.Errors_Handler_Helper.Features.TicketType.Queries.GetAllTicketTypes;
using Portal.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.TicketType.Commands.CreateTicketType
{
    public class CreateTicketTypeCommandHandler : IRequestHandler<CreateTicketTypeCommand, GetAllTicketTypesResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateTicketTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetAllTicketTypesResponseDto> Handle(CreateTicketTypeCommand request, CancellationToken cancellationToken)
        {
            var ticketType = _mapper.Map<Domains.Entities.TicketType>(request);
            await _unitOfWork.Repository<Domains.Entities.TicketType>().AddAsync(ticketType);
            if (await _unitOfWork.Complete() <= 0)
                throw new ApiErrorResponse(400, "Error Happen When Adding Type, Try Again Later.");
            return _mapper.Map<GetAllTicketTypesResponseDto>(ticketType);
        }
    }
}
