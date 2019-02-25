using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models
{
    public class OrderItem
    {
        public int ID { get; set; }
        public Order Order { get; set; }
        public Material Material { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public float Quantity { get; set; }
        public decimal CostPerUnit { get; set; }
    }
}
