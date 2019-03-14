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
        public float QuantityInStock { get; set; }
        public string Details { get; set; }

        public int CategoryId { get; set; }
        public MaterialCategory Category { get; set; }

        public int TypeId { get; set; }
        public MaterialType Type { get; set; }

        public int UnitOfMeasureId { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
    }
}
