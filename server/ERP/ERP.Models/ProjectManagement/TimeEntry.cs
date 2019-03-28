using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Models.Workorders;

namespace ERP.Models.ProjectManagement
{
    public class TimeEntry
    {
        public int Id { get; set; }
        public bool Billable { get; set; }
        public float Duration { get; set; }
        public string Notes { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TimeTypeId { get; set; }
        public TimeType TimeType { get; set; }
        // TODO: Determine if explicit foreign key is needed or not
        public Workorder Workorder { get; set; } // Optional
    }
}
