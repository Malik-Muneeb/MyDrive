using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;

namespace BAL
{
    public class folderBA
    {
        public bool saveFolder(folderDTO obj)
        {
            folderDAO folderObjDAO = new folderDAO();
            if (folderObjDAO.saveFolder(obj))
                return true;
            return false;
        }

        public List<folderDTO> getAllFolders(folderDTO obj)
        {
            folderDAO folderObjDAO = new folderDAO();
            return folderObjDAO.getAllFolders(obj);
        }

        public bool deleteFolder(int id)
        {
            folderDAO folderObjDAO = new folderDAO();
            if (folderObjDAO.deleteFolder(id))
                return true;
            return false;
        }

        public folderDTO getFolder(folderDTO obj)
        {
            folderDAO folderObjDAO = new folderDAO();
            return folderObjDAO.getFolder(obj);
        }

        public folderDTO getFolderById(int id)
        {
            folderDAO folderObjDAO = new folderDAO();
            return folderObjDAO.getFolderById(id);
        }

    }
}

