using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Entities;
using System.IO;
using BAL;

namespace Web_API.ApiControllers
{
    [EnableCors(origins: "http://localhost:1747", headers: "*", methods: "*")]

    public class fileDataController : ApiController
    {
        public bool addNewFile()
        {
            if (HttpContext.Current.Request.Files.Count > 0) 
            {
                try
                {
                    foreach(var fileName in HttpContext.Current.Request.Files.AllKeys)
                    {
                        HttpPostedFile file = HttpContext.Current.Request.Files[fileName];
                        if(file!=null)
                        {
                            fileDTO fileObj = new fileDTO();
                            fileObj.name = file.FileName;
                            fileObj.uniqueName = Guid.NewGuid().ToString();
                            fileObj.parentFolderId = Convert.ToInt32(System.Web.HttpContext.Current.Request["parentFolderId"]);
                            fileObj.fileExt = Path.GetExtension(file.FileName);
                            fileObj.fileSizeinKb = file.ContentLength / 1024;
                            fileObj.createdBy = Convert.ToInt32(System.Web.HttpContext.Current.Request["createdBy"]);
                            var rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");
                            var fileSavePath = System.IO.Path.Combine(rootPath, fileObj.uniqueName + fileObj.fileExt);
                            fileBA fileBAObj = new fileBA();
                            if (fileBAObj.saveFile(fileObj))
                            {
                                file.SaveAs(fileSavePath);
                                return true;
                            }
                                
                        }
                    }
                }
                catch(Exception ex)
                {
                    //
                }
            }
            return false;
        }
    }
}
