using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling.Domain.Common;
using Scheduling.Domain.Entities;

namespace Scheduling.Infrastructure.Persistence
{
    public class AppointmentSlotContext : DbContext
    {
        public AppointmentSlotContext(DbContextOptions<AppointmentSlotContext> options) : base(options)
        {
            
        }

        public DbSet<AppointmentSlot> AppointmentSlots { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "sysops@henrymeds.com";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "sysops@henrymeds.com";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
