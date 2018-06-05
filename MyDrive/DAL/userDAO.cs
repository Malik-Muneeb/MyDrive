using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;

namespace BAL
{
    public class userDAO
    {
        public bool validateUser(userDTO userObj)
        {
            String connString = @"Data Source=.\SQLEXPRESS2012; Initial Catalog=Assignment8; Integrated Security=True; Persist Security Info=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String sqlQuery = String.Format(@"Select * from dbo.users where login=@Login and password=@Password");
                SqlCommand command = new SqlCommand(sqlQuery, conn);
                SqlParameter param = command.Parameters.AddWithValue("@Login", userObj.txtLogin);
                if (userObj.txtLogin == null)
                    param.Value = DBNull.Value;
                param = command.Parameters.AddWithValue("@Password", userObj.txtPassword);
                if (userObj.txtPassword == null)
                    param.Value = DBNull.Value;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    return true;
            }
            return false;
        }
    }
}
