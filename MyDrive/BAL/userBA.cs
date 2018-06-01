using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DAL;

namespace BAL
{
    public class userBA
    {
        public bool validateUser(userDTO userObj)
        {
            userDAO userObjDAO = new userDAO();
            if(userObjDAO.validateUser(userObj))
                return true;
            return false;
        }
    }
}
