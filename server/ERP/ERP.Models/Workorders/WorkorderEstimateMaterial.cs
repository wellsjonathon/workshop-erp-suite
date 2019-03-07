using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Models.Inventory;

namespace ERP.Models.Workorders
{
    public class WorkorderEstimateMaterial
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal CostPerUnit { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }

        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }

        public int UnitOfMeasureId { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }

        public int WorkorderEstimateId { get; set; }
        public WorkorderEstimate WorkorderEstimate { get; set; }
}
}
