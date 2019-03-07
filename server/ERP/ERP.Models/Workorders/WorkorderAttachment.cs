using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Models.Workorders
{
    public class WorkorderAttachment
    {
        public int Id { get; set; }
        public string FileLocation { get; set; }
        
        public int WorkorderId { get; set; }
        public Workorder Workorder { get; set; }

        public int AttachmentTypeId { get; set; }
        public AttachmentType AttachmentType { get; set; }
    }
}
