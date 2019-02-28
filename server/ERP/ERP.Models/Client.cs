using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Faculty FacultyID { get; set; }
    }
}
