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

        public List<fileDTO> getAllFiles(fileDTO obj)
        {
            fileDAO fileObjDAO = new fileDAO();
            return fileObjDAO.getAllfiles(obj);
        }

        public bool deleteFile(int id)
        {
            fileDAO fileObjDAO = new fileDAO();
            if (fileObjDAO.deleteFile(id))
                return true;
            return false;
        }

        public fileDTO getFile(int id)
        {
            fileDAO fileObjDAO = new fileDAO();
            return fileObjDAO.getFile(id);
        }
    }
}
