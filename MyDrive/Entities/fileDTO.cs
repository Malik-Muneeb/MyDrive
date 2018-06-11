using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class fileDTO
    {
        public int id { get; set; }
        public String name { get; set; }
        public String uniqueName { get; set; }
        public int parentFolderId { get; set; }
        public String fileExt { get; set; }
        public int fileSizeinKb { get; set; }
        public int createdBy { get; set; }
        public DateTime uploadedOn { get; set; }
        public bool isActive { get; set; }
        public String contentType { get; set; }
    }
}
