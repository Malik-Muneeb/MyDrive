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
                sqlQuery = String.Format(@"INSERT INTO dbo.folder(name, parentfolderid,
                createdby,createdon,isactive) 
                VALUES('{0}','{1}','{2}','{3}','{4}')", obj.name, obj.parentFolderId, obj.createdBy, DateTime.Now,1);
            
                SqlCommand command = new SqlCommand(sqlQuery, conn);
                int rowAffected = command.ExecuteNonQuery();
                Console.WriteLine("{0} Row Affected", rowAffected);
                return true;
            }
        }

        public List<folderDTO> getAllFolders(folderDTO obj)
        {
            String connString = @"Data Source=.\SQLEXPRESS2012; Initial Catalog=Assignment8; Integrated Security=True; Persist Security Info=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String sqlQuery = "Select * from dbo.folder where createdBy='"+obj.createdBy+"' and parentfolderid='"+obj.parentFolderId+"' and isactive='1'";
                SqlCommand command = new SqlCommand(sqlQuery, conn);
                SqlDataReader reader = command.ExecuteReader();
                List<folderDTO> folderList = new List<folderDTO>();

                while (reader.Read() == true)
                {
                    folderDTO folderObj = new folderDTO();
                    folderObj.id = reader.GetInt32(reader.GetOrdinal("id"));
                    folderObj.name = reader.GetString(reader.GetOrdinal("name"));
                    folderObj.parentFolderId = reader.GetInt32(reader.GetOrdinal("parentfolderid"));
                    folderObj.createdBy = reader.GetInt32(reader.GetOrdinal("createdby"));
                    folderList.Add(folderObj);
                }
                return folderList;
            }
        }

        public bool deleteFolder(int id)
        {
            String connString = @"Data Source=.\SQLEXPRESS2012; Initial Catalog=Assignment8; Integrated Security=True; Persist Security Info=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String sqlQuery = "";
                sqlQuery = String.Format("Update dbo.folder Set isactive=0 Where id={0}", id);

                SqlCommand command = new SqlCommand(sqlQuery, conn);
                int rowAffected = command.ExecuteNonQuery();
                Console.WriteLine("{0} Row Affected", rowAffected);
                return true;
            }
        }

        public folderDTO getFolder(folderDTO obj)
        {
            String connString = @"Data Source=.\SQLEXPRESS2012; Initial Catalog=Assignment8; Integrated Security=True; Persist Security Info=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String sqlQuery = "Select * from dbo.folder where createdBy='" + obj.createdBy + "' and id='" + obj.parentFolderId + "' and isactive='1'";
                SqlCommand command = new SqlCommand(sqlQuery, conn);
                SqlDataReader reader = command.ExecuteReader();
                folderDTO folderObj = new folderDTO();

                if (reader.Read() == true)
                {
                    folderObj.id = reader.GetInt32(reader.GetOrdinal("id"));
                    folderObj.name = reader.GetString(reader.GetOrdinal("name"));
                    folderObj.parentFolderId = reader.GetInt32(reader.GetOrdinal("parentfolderid"));
                    folderObj.createdBy = reader.GetInt32(reader.GetOrdinal("createdby"));
                }
                return folderObj;
            }
        }

        public folderDTO getFolderById(int id)
        {
            String connString = @"Data Source=.\SQLEXPRESS2012; Initial Catalog=Assignment8; Integrated Security=True; Persist Security Info=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String sqlQuery = "Select * from dbo.folder where id='" + id + "' and isactive='1'";
                SqlCommand command = new SqlCommand(sqlQuery, conn);
                SqlDataReader reader = command.ExecuteReader();
                folderDTO folderObj = new folderDTO();

                if (reader.Read() == true)
                {
                    folderObj.id = reader.GetInt32(reader.GetOrdinal("id"));
                    folderObj.name = reader.GetString(reader.GetOrdinal("name"));
                    folderObj.parentFolderId = reader.GetInt32(reader.GetOrdinal("parentfolderid"));
                    folderObj.createdBy = reader.GetInt32(reader.GetOrdinal("createdby"));
                }
                return folderObj;
            }
        }
    }
}
