using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models.Workorders
{
    public class WorkorderNote
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
