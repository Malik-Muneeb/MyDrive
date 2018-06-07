using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using BAL;

namespace BAL
{
    public class userBA
    {
        public int validateUser(userDTO userObj)
        {
            userDAO userObjDAO = new userDAO();
            int id=userObjDAO.validateUser(userObj);
            if(id!=0)
                return id;
            return 0;
        }
    }
}
