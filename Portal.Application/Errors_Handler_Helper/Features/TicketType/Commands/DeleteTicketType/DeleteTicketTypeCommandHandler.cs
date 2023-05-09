using AutoMapper;
using MediatR;
using Portal.Application.Contracts;
using Portal.Application.Errors_Handler_Helper.Custom_Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.TicketType.Commands.DeleteTicketType
{
    public class DeleteTicketTypeCommandHandler : IRequestHandler<DeleteTicketTypeCommand, DeleteTicketTypeCommandResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTicketTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteTicketTypeCommandResponseDto> Handle(DeleteTicketTypeCommand request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.Repository<Domains.Entities.TicketType>().GetByIdAsync(request.Id);
            _unitOfWork.Repository<Domains.Entities.TicketType>().Delete(model);
            if (await _unitOfWork.Complete() <= 0)
            {
                throw new ApiErrorResponse(400, null);
            }
            return new DeleteTicketTypeCommandResponseDto() { IsSuccess = true };

        }
    }

}
