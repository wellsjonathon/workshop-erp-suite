using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models.Workorders
{
    public class WorkorderMaterial
    {
        public int Id { get; set; }
        public Workorder Workorder { get; set; }
        public Material Material { get; set; }
        public Vendor Vendor { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public float QuantityUsed { get; set; }
        public decimal CostPerUnit { get; set; }
    }
}
