using Portal.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Specifications.Ticket_Specifications
{
    public class TicketForUserByAccountIdSpecifications : BaseSpecification<Ticket>
    {
        public TicketForUserByAccountIdSpecifications(int id, string userId)
            :base(x => x.Id == id && x.UserId == userId)
        {
            AddInclude(x => x.TicketType);
        }
        public TicketForUserByAccountIdSpecifications(string userId)
            : base(x => x.UserId == userId)
        {
            AddInclude(x => x.TicketType);
        }
        public TicketForUserByAccountIdSpecifications()
        {
            AddInclude(x => x.TicketType);
            AddInclude(x => x.TicketAttachments);
        }
        public TicketForUserByAccountIdSpecifications(int ticketTypeId) : base (x => x.TicketTypeId== ticketTypeId)
        {
            AddInclude(x => x.TicketType);
            AddInclude(x => x.TicketAttachments);
        }
    }
}
