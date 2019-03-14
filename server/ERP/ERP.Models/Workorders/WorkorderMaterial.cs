using ERP.Models.Inventory;
using Newtonsoft.Json;

namespace ERP.Models.Workorders
{
    public class WorkorderMaterial
    {
        public int Id { get; set; }
        public float QuantityUsed { get; set; }
        public decimal? CostPerUnit { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }

        public int? VendorMaterialId { get; set; }
        public VendorMaterial VendorMaterial { get; set; }

        public int UnitOfMeasureId { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }

        [JsonIgnore]
        public int WorkorderId { get; set; }
        [JsonIgnore]
        public Workorder Workorder { get; set; }
    }

    public class WorkorderMaterialDto
    {
        public float QuantityUsed { get; set; }
        public decimal? CostPerUnit { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }

        public int? VendorId { get; set; }
        public Vendor Vendor { get; set; }

        public int? UnitOfMeasureId { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }

        public int WorkorderId { get; set; }
        public Workorder Workorder { get; set; }
    }
}
