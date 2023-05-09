using AutoMapper;
using MediatR;
using Portal.Application.Contracts;
using Portal.Application.Errors_Handler_Helper.Custom_Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.TicketType.Commands.UpdateTicketType
{
    public class UpdateTicketTypeCommandHandler : IRequestHandler<UpdateTicketTypeCommand, UpdateTicketTypeCommandResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateTicketTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UpdateTicketTypeCommandResponseDto> Handle(UpdateTicketTypeCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Domains.Entities.TicketType>(request);
            _unitOfWork.Repository<Domains.Entities.TicketType>().Update(model);
            if (await _unitOfWork.Complete() <= 0)
                throw new ApiErrorResponse(400, null);
            return _mapper.Map<UpdateTicketTypeCommandResponseDto>(request);
        }
    }
}
