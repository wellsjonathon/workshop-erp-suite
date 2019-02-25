using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models
{
    public class UnitOfMeasure
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // TODO: Look into expanding this
        // Potential options:
        //  Add "MeasurementSystem" table and FKs for imperial vs. metric
        //  Specify if it's a base unit or what it's base unit is
        //      Add a multiplier field in this case
    }
}
