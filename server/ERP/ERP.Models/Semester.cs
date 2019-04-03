using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models
{
    public class Semester
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int StartMonth { get; set; }
        public int EndMonth { get; set; }
    }
}
