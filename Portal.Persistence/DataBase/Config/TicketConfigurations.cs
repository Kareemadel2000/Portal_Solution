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
    public class TicketConfigurations : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.TicketTypeId).IsRequired();
            builder.Property(p => p.UserId).IsRequired();
            builder.HasOne(x => x.TicketType).WithMany().HasForeignKey(p => p.TicketTypeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
