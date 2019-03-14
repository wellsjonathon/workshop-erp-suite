using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Models.ProjectManagement;
using ERP.Models.Workflows;

namespace ERP.Models.Workorders
{
    // TODO: Clean this mess up...
    public class Workorder
    {
        public int Id { get; set; }

        public State State { get; set; }
        public WorkorderEstimate Estimate { get; set; }
        public List<TransitionHistory> TransitionHistory { get; set; }
        public List<WorkorderAttachment> Attachments { get; set; }
        public List<WorkorderMaterial> Materials { get; set; }
        public List<TimeEntry> TimeEntries { get; set; }
        public List<WorkorderComment> Comments { get; set; }
        public List<WorkorderNote> Notes { get; set; }

        // public Use Use { get; set; }
        // public Faculty Faculty { get; set; }
        // public Semester Semester { get; set; } // Enum? Is it even needed?
        public string Description { get; set; }
        public bool QuoteRequested { get; set; }

        // Is a "TotalOtherCosts" needed?
        public decimal TotalMaterialCost { get; set; }
        public decimal TotalLabourCost { get; set; }
        public decimal TotalCost { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateRequiredBy { get; set; }
        public DateTime DateCompleted { get; set; }
        public DateTime DatePickedUp { get; set; }

        // public TYPE SignatureForPickup { get; set; }

        // TODO: Change this over to grabbing the info from the client account
        //   when the requirements for that are determined
        public string ClientPhoneNumber { get; set; }
        public string ClientEmail { get; set; }
        public string ClientName { get; set; }

        // public TYPE ChargeAccount { get; set; }
        public string AuthorizedBy { get; set; }
        // public TYPE AuthorizedBySignature { get; set; }
        // public Invoice Invoice { get; set; }
    }
}
