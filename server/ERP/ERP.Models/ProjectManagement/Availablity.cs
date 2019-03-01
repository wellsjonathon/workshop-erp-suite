using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models.ProjectManagement
{
    public class Availablity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public string Notes { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public AvailabilityType AvailabilityTypeId { get; set; }
    }
}
