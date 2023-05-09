using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Domains.Entities
{
    public class TicketAttachment : BaseEntity
    {
        public string? ImageUrl { get; set; }
        public int TicketId { get; set; }
        public Ticket? Ticket { get; set; }
    }
}
