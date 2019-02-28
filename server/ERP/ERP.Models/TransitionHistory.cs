using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models
{
    public class TransitionHistory
    {
        public int ID { get; set; }
        public string Comment { get; set; }
        public string Description { get; set; }
        public Workflow WorkflowID { get; set; }
    }
}
