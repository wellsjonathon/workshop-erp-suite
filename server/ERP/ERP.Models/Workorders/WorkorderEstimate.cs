using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models.Workorders
{
    public class WorkorderEstimate
    {
        public int Id { get; set; }
        public int LabourTime { get; set; } // Days? Do we need to define this further?
        public decimal LabourCost { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal OtherCost { get; set; }
        public decimal TotalCost { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }

        public int WorkorderID { get; set; }
        public Workorder Workorder { get; set; }
    }
}
