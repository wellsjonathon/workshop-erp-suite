using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Models.Workflows;

namespace ERP.Models.Workorders
{
    public class TransitionHistory
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime Timestamp { get; set; }

        public int WorkorderId { get; set; }
        public Workorder Workorder { get; set; }

        public int TransitionId { get; set; }
        public Transition Transition { get; set; }
    }
}
