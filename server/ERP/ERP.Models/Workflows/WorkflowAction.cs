using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models.Workflows
{
    public class WorkflowAction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int WorkflowId { get; set; }
        public Workflow Workflow { get; set; }
    }
}
