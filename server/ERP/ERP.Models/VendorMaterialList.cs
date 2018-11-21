using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models
{
    public class VendorMaterialList
    {
        public int ID { get; set; }
        public Vendor Vendor { get; set; }
        public Material Material { get; set; }
        public decimal CostPerUnit { get; set; }
        public DateTime DateLastPurchased { get; set; }
    }
}
