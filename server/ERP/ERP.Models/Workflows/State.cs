using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models.Workflows
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ColorHex { get; set; }

        public int WorkflowId { get; set; }
        public Workflow Workflow { get; set; }
    }

    public class StateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    // TODO: Does this belong to TransitionHistory? Structure is a bit weird
    public class UpdateStateDto
    {
        public int NewStateId { get; set; }
        public string Comment { get; set; }
    }
}
