using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models
{
    public class TransitionAction
    {
        public int ID { get; set; }
        public Workflow WorkflowID { get; set; }
        public Action ActionID { get; set; }
        public Transition TransitionID { get; set; }
    }
}
