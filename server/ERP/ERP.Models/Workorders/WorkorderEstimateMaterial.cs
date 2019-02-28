using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models.Workorders
{
    public class WorkorderEstimateMaterial
    {
        public int ID { get; set; }
        public decimal Quantity { get; set; }
        public decimal CostPerUnit { get; set; }
        public Material MaterialID { get; set; }
        public Vendor VendorID { get; set; }
        public Unit UnitID { get; set; }
        public WorkorderEstimate WorkorderEstimateID { get; set; }
}
}
