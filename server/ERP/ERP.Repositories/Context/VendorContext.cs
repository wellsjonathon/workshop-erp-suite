using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ERP.Models;

namespace ERP.Repositories.Context
{
    public class VendorContext : DbContext
    {
        public VendorContext(DbContextOptions<VendorContext> options)
            : base(options)
        {
        }

        public DbSet<Vendor> Vendors { get; set; }
    }
}
