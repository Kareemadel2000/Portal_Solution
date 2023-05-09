using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketTypes_Client.Queries.GetTicketTypeById
{
    public class GetTicketTypeByIdQuery : IRequest<GenericTicketTypeResponseDto>
    {
        public int Id { get; set; }
    }
}
