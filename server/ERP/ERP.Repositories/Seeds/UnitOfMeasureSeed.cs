using ERP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Repositories.Seeds
{
    public class UnitOfMeasureSeed
    {
        static public void Seed(ModelBuilder builder)
        {
            builder.Entity<UnitOfMeasure>().HasData(
                new UnitOfMeasure { Id = 1, Name = "Inch" },
                new UnitOfMeasure { Id = 2, Name = "Foot" },
                new UnitOfMeasure { Id = 3, Name = "Yard" },
                new UnitOfMeasure { Id = 4, Name = "Centimeter" },
                new UnitOfMeasure { Id = 5, Name = "Meter" },
                new UnitOfMeasure { Id = 6, Name = "Ounce" },
                new UnitOfMeasure { Id = 7, Name = "Pound" },
                new UnitOfMeasure { Id = 8, Name = "Gram" },
                new UnitOfMeasure { Id = 9, Name = "Kilogram" },
                new UnitOfMeasure { Id = 10, Name = "Square Foot" },
                new UnitOfMeasure { Id = 11, Name = "Square Yard" },
                new UnitOfMeasure { Id = 12, Name = "Square Meter" });
        }
    }
}
