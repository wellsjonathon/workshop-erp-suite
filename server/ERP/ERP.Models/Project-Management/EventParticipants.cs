using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models.Project_Management
{
    public class EventParticipants
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Event EventId { get; set; }
    }
}
