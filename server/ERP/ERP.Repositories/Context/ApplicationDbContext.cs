using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ERP.Models;
using ERP.Models.Inventory;
using ERP.Models.Workflows;
using ERP.Models.Workorders;
using ERP.Models.ProjectManagement;
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
        public DbSet<WorkorderMaterial> WorkorderMaterials { get; set; }
        public DbSet<WorkorderComment> WorkorderComments { get; set; }
        public DbSet<WorkorderNote> WorkorderNotes { get; set; }
        public DbSet<TransitionHistory> TransitionHistory { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Purpose> Purposes { get; set; }
        public DbSet<Semester> Semesters { get; set; }

        // ===== Workflows =====
        public DbSet<Workflow> Workflows { get; set; }
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
        public DbSet<VendorMaterial> VendorMaterials { get; set; }

        // ===== Orders =====
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // ===== Units =====
        public DbSet<UnitOfMeasure> UnitsOfMeasure { get; set; }

        // ===== Project Management ====
        public DbSet<BillableOverrideType> BillableOverrideTypes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipants> EventParticipants { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }
        public DbSet<TimeType> TimeTypes { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<AvailabilityType> AvailabilityTypes { get; set; }

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

            builder.Entity<WorkorderMaterial>()
                .HasOne(m => m.UnitOfMeasure)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<VendorMaterial>()
                .HasOne(m => m.UnitOfMeasure)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OrderItem>()
                .HasOne(m => m.UnitOfMeasure)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TimeEntry>()
                .HasOne(e => e.Workorder)
                .WithMany(w => w.TimeEntries)
                .IsRequired(false);

            // TODO: Create specific seed functions
            WorkflowSeed.Seed(builder);
            UnitOfMeasureSeed.Seed(builder);

            // ===== Workorders =====
            builder.Entity<Faculty>().HasData(
                new Faculty { Id = 1, Name = "ISE" },
                new Faculty { Id = 2, Name = "ENSE" },
                new Faculty { Id = 3, Name = "SSE" },
                new Faculty { Id = 4, Name = "ESE" },
                new Faculty { Id = 5, Name = "PSE" }
            );

            builder.Entity<Purpose>().HasData(
                new Purpose { Id = 1, Name = "Research" },
                new Purpose { Id = 2, Name = "Class/Lab" },
                new Purpose { Id = 3, Name = "Other" }
            );

            builder.Entity<Semester>().HasData(
                new Semester { Id = 1, Name = "Winter", Code = "W", StartMonth = 1, EndMonth = 4 },
                new Semester { Id = 2, Name = "Spring/Summer", Code = "S", StartMonth = 5, EndMonth = 8 },
                new Semester { Id = 3, Name = "Fall", Code = "F", StartMonth = 9, EndMonth = 12 }
            );

            // ===== Materials =====
            builder.Entity<MaterialCategory>().HasData(
                new MaterialCategory { Id = 1, Name = "Rod" },
                new MaterialCategory { Id = 2, Name = "Plate" },
                new MaterialCategory { Id = 3, Name = "Sheet" }
            );
            builder.Entity<MaterialType>().HasData(
                new MaterialType { Id = 1, Name = "Aluminum" },
                new MaterialType { Id = 2, Name = "Steel" },
                new MaterialType { Id = 3, Name = "Nylon" }
            );

            // ===== Project Management =====
            builder.Entity<TimeType>().HasData(
                new TimeType { Id = 1, Name = "Shop", CostPerHour = 10 },
                new TimeType { Id = 2, Name = "Office", CostPerHour = 15 },
                new TimeType { Id = 3, Name = "Meeting", CostPerHour = 20 }
            );
            builder.Entity<BillableOverrideType>().HasData(
                new BillableOverrideType { Id = 1, Name = "Undergraduate" },
                new BillableOverrideType { Id = 2, Name = "Graduate" }
                // More if needed
            );
            builder.Entity<AvailabilityType>().HasData(
                new AvailabilityType { Id = 1, Name = "Vacation" },
                new AvailabilityType { Id = 2, Name = "Sick" },
                new AvailabilityType { Id = 3, Name = "Shop Closed" }
                // More needed 
            );
        }
    }
}
