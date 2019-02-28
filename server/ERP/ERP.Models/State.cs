using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models
{
    public class State
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Colour { get; set; } //Canada Baby
        public Workflow WorkflowID { get; set; }
    }
}
