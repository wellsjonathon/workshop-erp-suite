using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models.Workflows
{
    public class Transition
    {
        // TODO: Probably better suited using a combination key with CurrentStateId and NextStateId
        public int Id { get; set; }

        public int WorkflowId { get; set; }
        public Workflow Workflow { get; set; }

        public int CurrentStateId { get; set; }
        public State CurrentState { get; set; }

        public int NextStateId { get; set; }
        public State NextState { get; set; }
    }

    public class TransitionDto
    {
        public int Id { get; set; }

        public StateDto NextState { get; set; }
    }
}