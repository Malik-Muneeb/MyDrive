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

        public List<folderDTO> getAllFolders(int createdBy)
        {
            folderDAO folderObjDAO = new folderDAO();
            return folderObjDAO.getAllFolders(createdBy);
        }
    }
}

