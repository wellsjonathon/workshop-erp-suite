using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models
{
    public class TimeEntry
    {
        public int ID { get; set; }
        public TimeType TimeType { get; set; }
        public Workorder Workorder { get; set; } // Optional
        public bool Billable { get; set; }
        public float Duration { get; set; }
        public string Notes { get; set; }
        public DateTime DateTime { get; set; }
    }
}
