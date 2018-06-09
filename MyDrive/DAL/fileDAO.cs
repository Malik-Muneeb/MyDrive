using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class fileDAO
    {
        public bool saveFile(fileDTO obj)
        {
            String connString = @"Data Source=.\SQLEXPRESS2012; Initial Catalog=Assignment8; Integrated Security=True; Persist Security Info=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String sqlQuery = "";

                sqlQuery = String.Format(@"INSERT INTO dbo.files(name, uniquename, 
                parentfolderid, fileext, filesizeinkb, createdby, uploadedon, isactive) 
                VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", obj.name,
                obj.uniqueName, obj.parentFolderId, obj.fileExt, obj.fileSizeinKb,
                obj.createdBy, DateTime.Now, 1);


                SqlCommand command = new SqlCommand(sqlQuery, conn);
                int rowAffected = command.ExecuteNonQuery();
                Console.WriteLine("{0} Row Affected", rowAffected);
                return true;
            }
        }
    }
}
