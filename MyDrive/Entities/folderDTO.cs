using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class folderDTO
    {
        public int id { get; set; }
        public String name { get; set; }
        public int parentFolderId { get; set; }
        public int createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public bool isActive { get; set; }
    }
}
