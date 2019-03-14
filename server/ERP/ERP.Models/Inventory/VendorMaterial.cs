using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models.Inventory
{
    public class VendorMaterial
    {
        public int Id { get; set; }
        public decimal CostPerUnit { get; set; }
        public DateTime? DateLastPurchased { get; set; }

        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }

        public int UnitOfMeasureId { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
    }

    public class VendorMaterialDto
    {
        public decimal CostPerUnit { get; set; }
        public int VendorId { get; set; }
        public int MaterialId { get; set; }
        public int? UnitOfMeasureId { get; set; }
    }
}
