using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class fileBA
    {
        public bool saveFile(fileDTO obj)
        {
            fileDAO fileObjDAO = new fileDAO();
            if (fileObjDAO.saveFile(obj))
                return true;
            return false;
        }
    }
}
