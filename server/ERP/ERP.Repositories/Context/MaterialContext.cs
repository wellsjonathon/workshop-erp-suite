using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ERP.Models;

namespace ERP.Repositories.Context
{
    public class MaterialContext : DbContext
    {
        public MaterialContext(DbContextOptions<MaterialContext> options)
            : base(options)
        { }

        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialCategory> MaterialCategories { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MaterialCategory>().HasData(
                new MaterialCategory { ID = 1, Name = "Rod" },
                new MaterialCategory { ID = 2, Name = "Plate" },
                new MaterialCategory { ID = 3, Name = "Sheet"  }
            );

            modelBuilder.Entity<MaterialType>().HasData(
                new MaterialType { ID = 1, Name = "Aluminum"},
                new MaterialType { ID = 2, Name = "Steel"},
                new MaterialType { ID = 3, Name = "Nylon"}
                );
        }
    }
}
