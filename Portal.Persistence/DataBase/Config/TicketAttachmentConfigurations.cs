using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Persistence.DataBase.Config
{
    public class TicketAttachmentConfigurations : IEntityTypeConfiguration<TicketAttachment>
    {
        public void Configure(EntityTypeBuilder<TicketAttachment> builder)
        {
            builder.Property(p => p.ImageUrl).IsRequired();
            builder.Property(p => p.TicketId).IsRequired();            
        }
    }
}
