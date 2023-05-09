using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Domains.Entities
{
    public class Ticket : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public DateTime CreationAt { get; set; } = DateTime.Now;
        public int TicketTypeId { get; set; }
        public string UserId { get; set; }        
        public TicketType? TicketType { get; set; }
        public List<TicketAttachment?>? TicketAttachments { get; set; } = new();

    }
}
