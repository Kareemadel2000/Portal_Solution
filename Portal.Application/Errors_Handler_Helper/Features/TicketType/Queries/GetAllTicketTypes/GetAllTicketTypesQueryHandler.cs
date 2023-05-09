using AutoMapper;
using MediatR;
using Portal.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.TicketType.Queries.GetAllTicketTypes
{
    public class GetAllTicketTypesQueryHandler : IRequestHandler<GetAllTicketTypesQuery, IReadOnlyList<GetAllTicketTypesResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTicketTypesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<GetAllTicketTypesResponseDto>> Handle(GetAllTicketTypesQuery request, CancellationToken cancellationToken)
        {
            var AllTicketTypes = await _unitOfWork.Repository<Domains.Entities.TicketType>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<GetAllTicketTypesResponseDto>>(AllTicketTypes);
        }
    }
}
