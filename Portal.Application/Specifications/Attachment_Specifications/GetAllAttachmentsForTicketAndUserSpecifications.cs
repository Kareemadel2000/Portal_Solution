using Portal.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Specifications.Attachment_Specifications
{
    public class GetAllAttachmentsForTicketAndUserSpecifications : BaseSpecification<TicketAttachment>
    {
        public GetAllAttachmentsForTicketAndUserSpecifications(int ticketId):base(x => x.TicketId == ticketId)
        {
        }
        public GetAllAttachmentsForTicketAndUserSpecifications(int id, int ticketId):base(x => x.Id == id && x.TicketId == ticketId)
        {
        }
    }
}
