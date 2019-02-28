using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models
{
    public class WorkorderSchedule
    {
        public int ID { get; set }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
