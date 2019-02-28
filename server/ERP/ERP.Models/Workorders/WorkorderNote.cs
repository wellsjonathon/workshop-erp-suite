using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models.Workorders
{
    public class WorkorderNote
    {
        public int ID { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
    }
}
