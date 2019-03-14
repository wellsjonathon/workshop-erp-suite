using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Models.Workorders;

namespace ERP.Models.Inventory
{
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime DateOrdered { get; set; }
        public DateTime DateReceived { get; set; }

        public List<OrderItem> Items { get; set; }

        // TODO: This is optional, determine if it is even needed
        // public int WorkorderId { get; set; }
        // public Workorder Workorder { get; set; }

        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
    }
}
