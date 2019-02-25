using ERP.Models.Workflows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Repositories.Seeds
{
    public class WorkflowSeed
    {
        static public void Seed(ModelBuilder builder)
        {
            builder.Entity<Workflow>().HasData(
                new Workflow { Id = 1, Name = "Default", Active = true });

            builder.Entity<State>().HasData(
                new State { Id = 1, Name = "New", Description = "", ColorHex = "", WorkflowId = 1},
                new State { Id = 2, Name = "In review", Description = "", ColorHex = "", WorkflowId = 1 },
                new State { Id = 3, Name = "Scheduled", Description = "", ColorHex = "", WorkflowId = 1 },
                new State { Id = 4, Name = "In progress", Description = "", ColorHex = "", WorkflowId = 1 },
                new State { Id = 5, Name = "Ready for pickup", Description = "", ColorHex = "", WorkflowId = 1 },
                new State { Id = 6, Name = "Closed", Description = "", ColorHex = "", WorkflowId = 1 },
                new State { Id = 7, Name = "Waiting", Description = "", ColorHex = "", WorkflowId = 1 },
                new State { Id = 8, Name = "Cancelled", Description = "", ColorHex = "", WorkflowId = 1 },
                new State { Id = 9, Name = "Rejected", Description = "", ColorHex = "", WorkflowId = 1 });

            builder.Entity<Transition>().HasData(
                // ===== General flow =====
                new Transition { Id = 1, CurrentStateId = 1, NextStateId = 2, WorkflowId = 1 },
                new Transition { Id = 2, CurrentStateId = 2, NextStateId = 3, WorkflowId = 1 },
                new Transition { Id = 3, CurrentStateId = 3, NextStateId = 4, WorkflowId = 1 },
                new Transition { Id = 4, CurrentStateId = 4, NextStateId = 5, WorkflowId = 1 },
                new Transition { Id = 5, CurrentStateId = 5, NextStateId = 6, WorkflowId = 1 },
                // ===== To waiting =====
                new Transition { Id = 6, CurrentStateId = 1, NextStateId = 7, WorkflowId = 1 },
                new Transition { Id = 7, CurrentStateId = 2, NextStateId = 7, WorkflowId = 1 },
                new Transition { Id = 8, CurrentStateId = 3, NextStateId = 7, WorkflowId = 1 },
                new Transition { Id = 9, CurrentStateId = 4, NextStateId = 7, WorkflowId = 1 },
                new Transition { Id = 10, CurrentStateId = 5, NextStateId = 7, WorkflowId = 1 },
                // ===== From waiting =====
                new Transition { Id = 11, CurrentStateId = 7, NextStateId = 1, WorkflowId = 1 },
                new Transition { Id = 12, CurrentStateId = 7, NextStateId = 2, WorkflowId = 1 },
                new Transition { Id = 13, CurrentStateId = 7, NextStateId = 3, WorkflowId = 1 },
                new Transition { Id = 14, CurrentStateId = 7, NextStateId = 4, WorkflowId = 1 },
                new Transition { Id = 15, CurrentStateId = 7, NextStateId = 5, WorkflowId = 1 },
                new Transition { Id = 16, CurrentStateId = 7, NextStateId = 6, WorkflowId = 1 },
                // ===== To cancelled =====
                new Transition { Id = 17, CurrentStateId = 1, NextStateId = 8, WorkflowId = 1 },
                new Transition { Id = 18, CurrentStateId = 2, NextStateId = 8, WorkflowId = 1 },
                new Transition { Id = 19, CurrentStateId = 3, NextStateId = 8, WorkflowId = 1 },
                new Transition { Id = 20, CurrentStateId = 4, NextStateId = 8, WorkflowId = 1 },
                new Transition { Id = 21, CurrentStateId = 5, NextStateId = 8, WorkflowId = 1 },
                new Transition { Id = 22, CurrentStateId = 7, NextStateId = 8, WorkflowId = 1 },
                // ===== To rejected =====
                new Transition { Id = 23, CurrentStateId = 1, NextStateId = 9, WorkflowId = 1 },
                new Transition { Id = 24, CurrentStateId = 2, NextStateId = 9, WorkflowId = 1 },
                new Transition { Id = 25, CurrentStateId = 3, NextStateId = 9, WorkflowId = 1 },
                new Transition { Id = 26, CurrentStateId = 4, NextStateId = 9, WorkflowId = 1 },
                new Transition { Id = 27, CurrentStateId = 5, NextStateId = 9, WorkflowId = 1 },
                new Transition { Id = 28, CurrentStateId = 7, NextStateId = 9, WorkflowId = 1 });

            builder.Entity<WorkflowAction>().HasData(
                // ===== General flow =====
                new WorkflowAction { Id = 1, Name = "Reviewing", Description = "", WorkflowId = 1 },
                new WorkflowAction { Id = 2, Name = "Accepted", Description = "", WorkflowId = 1 },
                new WorkflowAction { Id = 3, Name = "Begin work", Description = "", WorkflowId = 1 },
                new WorkflowAction { Id = 4, Name = "Finish work", Description = "", WorkflowId = 1 },
                new WorkflowAction { Id = 5, Name = "Finish workorder", Description = "", WorkflowId = 1 },
                // ===== To waiting =====
                new WorkflowAction { Id = 6, Name = "Waiting", Description = "", WorkflowId = 1 },
                // ===== From waiting =====
                new WorkflowAction { Id = 7, Name = "Back to new", Description = "", WorkflowId = 1 },
                new WorkflowAction { Id = 8, Name = "Back to reviewing", Description = "", WorkflowId = 1 },
                new WorkflowAction { Id = 9, Name = "Back to accepted", Description = "", WorkflowId = 1 },
                new WorkflowAction { Id = 10, Name = "Back to work", Description = "", WorkflowId = 1 },
                new WorkflowAction { Id = 11, Name = "Ready for pickup", Description = "", WorkflowId = 1 },
                // ===== To cancelled =====
                new WorkflowAction { Id = 12, Name = "Cancel workorder", Description = "", WorkflowId = 1 },
                // ===== To rejected =====
                new WorkflowAction { Id = 13, Name = "Reject workorder", Description = "", WorkflowId = 1 });

            builder.Entity<TransitionAction>().HasData(
                // ===== General flow =====
                new TransitionAction { Id = 1, TransitionId = 1, WorkflowActionId = 1, WorkflowId = 1 },
                new TransitionAction { Id = 2, TransitionId = 2, WorkflowActionId = 2, WorkflowId = 1 },
                new TransitionAction { Id = 3, TransitionId = 3, WorkflowActionId = 3, WorkflowId = 1 },
                new TransitionAction { Id = 4, TransitionId = 4, WorkflowActionId = 4, WorkflowId = 1 },
                new TransitionAction { Id = 5, TransitionId = 5, WorkflowActionId = 5, WorkflowId = 1 },
                // ===== To waiting =====
                new TransitionAction { Id = 6, TransitionId = 6, WorkflowActionId = 6, WorkflowId = 1 },
                new TransitionAction { Id = 7, TransitionId = 7, WorkflowActionId = 6, WorkflowId = 1 },
                new TransitionAction { Id = 8, TransitionId = 8, WorkflowActionId = 6, WorkflowId = 1 },
                new TransitionAction { Id = 9, TransitionId = 9, WorkflowActionId = 6, WorkflowId = 1 },
                new TransitionAction { Id = 10, TransitionId = 10, WorkflowActionId = 6, WorkflowId = 1 },
                // ===== From waiting =====
                new TransitionAction { Id = 11, TransitionId = 11, WorkflowActionId = 7, WorkflowId = 1 },
                new TransitionAction { Id = 12, TransitionId = 12, WorkflowActionId = 8, WorkflowId = 1 },
                new TransitionAction { Id = 13, TransitionId = 13, WorkflowActionId = 9, WorkflowId = 1 },
                new TransitionAction { Id = 14, TransitionId = 14, WorkflowActionId = 10, WorkflowId = 1 },
                new TransitionAction { Id = 15, TransitionId = 15, WorkflowActionId = 11, WorkflowId = 1 },
                new TransitionAction { Id = 16, TransitionId = 16, WorkflowActionId = 5, WorkflowId = 1 },
                // ===== To cancelled =====
                new TransitionAction { Id = 17, TransitionId = 17, WorkflowActionId = 12, WorkflowId = 1 },
                new TransitionAction { Id = 18, TransitionId = 18, WorkflowActionId = 12, WorkflowId = 1 },
                new TransitionAction { Id = 19, TransitionId = 19, WorkflowActionId = 12, WorkflowId = 1 },
                new TransitionAction { Id = 20, TransitionId = 20, WorkflowActionId = 12, WorkflowId = 1 },
                new TransitionAction { Id = 21, TransitionId = 21, WorkflowActionId = 12, WorkflowId = 1 },
                new TransitionAction { Id = 22, TransitionId = 22, WorkflowActionId = 12, WorkflowId = 1 },
                // ===== To rejected =====
                new TransitionAction { Id = 23, TransitionId = 23, WorkflowActionId = 13, WorkflowId = 1 },
                new TransitionAction { Id = 24, TransitionId = 24, WorkflowActionId = 13, WorkflowId = 1 },
                new TransitionAction { Id = 25, TransitionId = 25, WorkflowActionId = 13, WorkflowId = 1 },
                new TransitionAction { Id = 26, TransitionId = 26, WorkflowActionId = 13, WorkflowId = 1 },
                new TransitionAction { Id = 27, TransitionId = 27, WorkflowActionId = 13, WorkflowId = 1 },
                new TransitionAction { Id = 28, TransitionId = 28, WorkflowActionId = 13, WorkflowId = 1 });
        }
    }
}
