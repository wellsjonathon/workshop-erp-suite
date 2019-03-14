using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models.Inventory
{
    public class VendorMaterialList
    {
        public int Id { get; set; }
        public decimal CostPerUnit { get; set; }
        public DateTime DateLastPurchased { get; set; }

        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
