using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models.Inventory
{
    public class OrderItem
    {
        public int Id { get; set; }
        public float Quantity { get; set; }
        public decimal CostPerUnit { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }

        public int UnitOfMeasureId { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
    }
}
