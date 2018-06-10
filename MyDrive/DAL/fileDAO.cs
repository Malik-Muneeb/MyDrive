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
                parentfolderid, fileext, filesizeinkb, createdby, uploadedon, contenttype, isactive) 
                VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8})", obj.name,
                obj.uniqueName, obj.parentFolderId, obj.fileExt, obj.fileSizeinKb,
                obj.createdBy, DateTime.Now, obj.contentType, 1);


                SqlCommand command = new SqlCommand(sqlQuery, conn);
                int rowAffected = command.ExecuteNonQuery();
                Console.WriteLine("{0} Row Affected", rowAffected);
                return true;
            }
        }

        public List<fileDTO> getAllfiles(fileDTO obj)
        {
            String connString = @"Data Source=.\SQLEXPRESS2012; Initial Catalog=Assignment8; Integrated Security=True; Persist Security Info=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String sqlQuery = "Select * from dbo.files where createdBy='" + obj.createdBy + "' and parentfolderid='" + obj.parentFolderId + "' and isactive='1'";
                SqlCommand command = new SqlCommand(sqlQuery, conn);
                SqlDataReader reader = command.ExecuteReader();
                List<fileDTO> fileList = new List<fileDTO>();

                while (reader.Read() == true)
                {
                    fileDTO fileObj = new fileDTO();
                    fileObj.id = reader.GetInt32(reader.GetOrdinal("id"));
                    fileObj.name = reader.GetString(reader.GetOrdinal("name"));
                    fileObj.uniqueName = reader.GetString(reader.GetOrdinal("uniquename"));
                    fileObj.parentFolderId = reader.GetInt32(reader.GetOrdinal("parentfolderid"));
                    fileObj.fileExt = reader.GetString(reader.GetOrdinal("fileext"));
                    fileObj.fileSizeinKb = reader.GetInt32(reader.GetOrdinal("filesizeinkb"));
                    if (!reader.IsDBNull(reader.GetOrdinal("contenttype")))
                    {
                        fileObj.contentType = reader.GetString(reader.GetOrdinal("contenttype"));
                    }
                    else
                    {
                        fileObj.contentType = null;
                    }
                    fileList.Add(fileObj);
                }
                return fileList;
            }
        }

        public bool deleteFile(int id)
        {
            String connString = @"Data Source=.\SQLEXPRESS2012; Initial Catalog=Assignment8; Integrated Security=True; Persist Security Info=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String sqlQuery = "";
                sqlQuery = String.Format("Update dbo.files Set isactive=0 Where id={0}", id);

                SqlCommand command = new SqlCommand(sqlQuery, conn);
                int rowAffected = command.ExecuteNonQuery();
                Console.WriteLine("{0} Row Affected", rowAffected);
                return true;
            }
        }

        public fileDTO getFile(String uniqueName)
        {
            String connString = @"Data Source=.\SQLEXPRESS2012; Initial Catalog=Assignment8; Integrated Security=True; Persist Security Info=True;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String sqlQuery = "Select * from dbo.files where uniquename='" + uniqueName + "'and isactive='1'";
                SqlCommand command = new SqlCommand(sqlQuery, conn);
                SqlDataReader reader = command.ExecuteReader();
                fileDTO fileObj = new fileDTO();
                if (reader.Read() == true)
                {
                    fileObj.id = reader.GetInt32(reader.GetOrdinal("id"));
                    fileObj.name = reader.GetString(reader.GetOrdinal("name"));
                    fileObj.uniqueName = reader.GetString(reader.GetOrdinal("uniquename"));
                    fileObj.parentFolderId = reader.GetInt32(reader.GetOrdinal("parentfolderid"));
                    fileObj.fileExt = reader.GetString(reader.GetOrdinal("fileext"));
                    fileObj.fileSizeinKb = reader.GetInt32(reader.GetOrdinal("filesizeinkb"));
                    if (!reader.IsDBNull(reader.GetOrdinal("contenttype")))
                    {
                        fileObj.contentType = reader.GetString(reader.GetOrdinal("contenttype"));
                    }
                    else
                    {
                        fileObj.contentType = null;
                    }
                }
                return fileObj;
            }
        }
    }
}
