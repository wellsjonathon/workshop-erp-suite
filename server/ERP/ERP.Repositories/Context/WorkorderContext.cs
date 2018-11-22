using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ERP.Models;

namespace ERP.Repositories.Context
{
    public class WorkorderContext : DbContext
    {
        public WorkorderContext(DbContextOptions<WorkorderContext> options)
            : base(options)
        {
        }

        public DbSet<Workorder> Workorders { get; set; }
        public DbSet<WorkorderStatus> WorkorderStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkorderStatus>().HasData(
                new WorkorderStatus { ID = 1, Name = "New" },
                new WorkorderStatus { ID = 2, Name = "Scheduled" },
                new WorkorderStatus { ID = 3, Name = "In Progress" },
                new WorkorderStatus { ID = 4, Name = "Completed" },
                new WorkorderStatus { ID = 5, Name = "Rejected" },
                new WorkorderStatus { ID = 6, Name = "Cancelled" }
            );
        }
    }
}
