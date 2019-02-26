using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ERP.Models;
using ERP.Models.Workflows;
using ERP.Models.Workorders;
using ERP.Repositories.Seeds;

namespace ERP.Repositories.Context
{
    // TODO: IdentityDbContext can be templated with custom Account object, determine if needed
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ===== Workorders =====
        public DbSet<Workorder> Workorders { get; set; }
        public DbSet<WorkorderStatus> WorkorderStatuses { get; set; }
        public DbSet<WorkorderMaterial> WorkorderMaterials { get; set; }
        public DbSet<TransitionHistory> TransitionHistory { get; set; }

        // ===== Workflows =====
        public DbSet<Workflow> Worflows { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Transition> Transitions { get; set; }
        public DbSet<WorkflowAction> Actions { get; set; }
        public DbSet<TransitionAction> TransitionActions { get; set; }

        // ===== Materials =====
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialCategory> MaterialCategories { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }

        // ===== Vendors =====
        public DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Call OnModelCreating of IdentityDbContext to create identity management tables
            base.OnModelCreating(builder);

            builder.Entity<Transition>()
                .HasOne(t => t.CurrentState)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Transition>()
                .HasOne(t => t.NextState)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            // TODO: Create specific seed functions
            WorkflowSeed.Seed(builder);

            // ===== Workorders =====
            builder.Entity<WorkorderStatus>().HasData(
                new WorkorderStatus { ID = 1, Name = "New" },
                new WorkorderStatus { ID = 2, Name = "Scheduled" },
                new WorkorderStatus { ID = 3, Name = "In Progress" },
                new WorkorderStatus { ID = 4, Name = "Completed" },
                new WorkorderStatus { ID = 5, Name = "Rejected" },
                new WorkorderStatus { ID = 6, Name = "Cancelled" }
            );

            // ===== Materials =====
            builder.Entity<MaterialCategory>().HasData(
                new MaterialCategory { ID = 1, Name = "Rod" },
                new MaterialCategory { ID = 2, Name = "Plate" },
                new MaterialCategory { ID = 3, Name = "Sheet" }
            );
            builder.Entity<MaterialType>().HasData(
                new MaterialType { ID = 1, Name = "Aluminum" },
                new MaterialType { ID = 2, Name = "Steel" },
                new MaterialType { ID = 3, Name = "Nylon" }
            );
        }
    }
}
