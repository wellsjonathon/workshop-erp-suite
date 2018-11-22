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
    }
}
