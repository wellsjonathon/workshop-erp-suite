using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models
{
    public class BillableOverrideType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal CostPerHour { get; set; }
        public decimal OverrideModValue { get; set; }
        // More?
    }
}
