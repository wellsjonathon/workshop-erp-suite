using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models.Workflows
{
    public class TransitionAction
    {
        public int Id { get; set; }

        public int WorkflowId { get; set; }
        public Workflow Workflow { get; set; }

        public int WorkflowActionId { get; set; }
        public WorkflowAction WorkflowAction { get; set; }

        public int TransitionId { get; set; }
        public Transition Transition { get; set; }
    }
}
