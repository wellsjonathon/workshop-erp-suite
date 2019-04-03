using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models.Workorders
{
    public class WorkorderComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime Timestamp { get; set; }
        // TODO: Rework when proper user management is added
        public string Username { get; set; }

        [JsonIgnore]
        public int WorkorderId { get; set; }
        [JsonIgnore]
        public Workorder Workorder { get; set; }
    }
}
