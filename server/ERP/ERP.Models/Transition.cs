using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models
{
    public class Transition
    {
        public int ID { get; set; }
        public Workflow WorkflowID { get; set; }
        public State CurrentStateID { get; set; }
        public State NextStateID { get; set; }
    }
}
