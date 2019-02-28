using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models
{
    public class WorkorderEstimate
    {
        public int ID { get; set; }
        public decimal MaterialCost { get; set; }
        public int LabourTime { get; set; }
        public decimal LabourCost { get; set; }
        public decimal OtherCost { get; set; }
        public decimal TotalCost { get; set; }
        public Workorder WorkorderID { get; set; }
    }
}
