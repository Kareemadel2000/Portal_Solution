using AutoMapper;
using MediatR;
using Portal.Application.Contracts;
using Portal.Application.Errors_Handler_Helper.Features.TicketType.Queries.GetAllTicketTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.TicketType.Queries.GetTiketTypeById
{
    public class GetTicketTypByIdQueryHandler : IRequestHandler<GetTicketTypByIdQuery, GetAllTicketTypesResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTicketTypByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetAllTicketTypesResponseDto> Handle(GetTicketTypByIdQuery request, CancellationToken cancellationToken)
        {
            var type = await _unitOfWork.Repository<Domains.Entities.TicketType>().GetByIdAsync(request.Id);
            return _mapper.Map<GetAllTicketTypesResponseDto>(type);
        }
    }
}
