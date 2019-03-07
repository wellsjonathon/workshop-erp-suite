using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models.Inventory
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual MaterialCategory Category { get; set; }
        public virtual MaterialType Type { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public float QuantityInStock { get; set; }
        public string Details { get; set; }
    }
}
