using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;

namespace BAL
{
    public class folderDAO
    {
        public bool saveFolder(folderDTO obj)
        {
            String connString = @"Data Source=.\SQLEXPRESS2012; Initial Catalog=Assignment8; Integrated Security=True; Persist Security Info=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String sqlQuery = "";
                if (obj.id > 0)
                {
//                    sqlQuery = String.Format(@"Update dbo.folders Set name='{0}',login='{1}', password='{2}',email='{3}',
//                              gender='{4}',address='{5}',age='{6}',nic='{7}',dob='{8}',iscricket='{9}',hockey='{10}',
//                              chess='{11}',imagename='{12}' Where folderid='{13}'", obj.txtName, obj.txtLogin, obj.txtPassword,
//                               obj.txtEmail, obj.cmbGender, obj.txtAddress, obj.txtAge, obj.txtCnic, obj.dateDob, obj.chkCricket,
//                               obj.chkHockey, obj.chkChess, obj.folderImage, obj.txtId);
                }
                else
                {
                    sqlQuery = String.Format(@"INSERT INTO dbo.folder(name, parentfolderid,
                createdby,createdon,isactive) 
                VALUES('{0}','{1}','{2}','{3}','{4}')", obj.name, obj.parentFolderId, obj.createdBy, DateTime.Now,1);
                }


                SqlCommand command = new SqlCommand(sqlQuery, conn);
                int rowAffected = command.ExecuteNonQuery();
                Console.WriteLine("{0} Row Affected", rowAffected);
                return true;
            }
        }

        public List<folderDTO> getAllFolders(int createdBy)
        {
            String connString = @"Data Source=.\SQLEXPRESS2012; Initial Catalog=Assignment8; Integrated Security=True; Persist Security Info=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String sqlQuery = "Select * from dbo.folder where createdBy='"+createdBy+"'";
                SqlCommand command = new SqlCommand(sqlQuery, conn);
                SqlDataReader reader = command.ExecuteReader();
                List<folderDTO> folderList = new List<folderDTO>();

                while (reader.Read() == true)
                {
                    folderDTO folderObj = new folderDTO();
                    folderObj.id = reader.GetInt32(reader.GetOrdinal("id"));
                    folderObj.name = reader.GetString(reader.GetOrdinal("name"));
                    folderObj.parentFolderId = reader.GetInt32(reader.GetOrdinal("parentfolderid"));                    
                    folderList.Add(folderObj);
                }
                return folderList;
            }
        }
    }
}
