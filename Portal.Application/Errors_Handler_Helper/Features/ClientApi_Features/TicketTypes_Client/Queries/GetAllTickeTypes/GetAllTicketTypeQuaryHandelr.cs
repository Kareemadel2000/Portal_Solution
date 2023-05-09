using AutoMapper;
using MediatR;
using Portal.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketTypes_Client.Queries.GetAllTickeTypes
{
    public class GetAllTicketTypeQuaryHandelr : IRequestHandler<GetAllTicketTypeQueary, IReadOnlyList<GenericTicketTypeResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllTicketTypeQuaryHandelr(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<GenericTicketTypeResponseDto>> Handle(GetAllTicketTypeQueary request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Repository<Domains.Entities.TicketType>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<GenericTicketTypeResponseDto>>(data);
        }
    }
}
