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
using System.Net.Http.Headers;
using Microsoft.WindowsAPICodePack.Shell;
using System.Drawing;

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
                            fileObj.contentType = file.ContentType;
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

        [HttpPost]
        public List<fileDTO> loadFiles()
        {
            fileDTO obj = new fileDTO();
            obj.createdBy = Convert.ToInt32(System.Web.HttpContext.Current.Request["createdBy"]);
            obj.parentFolderId = Convert.ToInt32(System.Web.HttpContext.Current.Request["parentFolderId"]);
            fileBA fileBAObj = new fileBA();
            List<fileDTO> fileList = fileBAObj.getAllFiles(obj);
            return fileList;
        }

        [HttpPost]
        public bool deleteFile()
        {
            int id = Convert.ToInt32(System.Web.HttpContext.Current.Request["id"]);
            fileBA fileBAObj = new fileBA();
            if (fileBAObj.deleteFile(id))
                return true;
            return false;
        }

        [HttpGet]
        public HttpResponseMessage downloadFile(String uniqueName)
        {
            fileBA fileBAObj = new fileBA();
            fileDTO fileObj = fileBAObj.getFile(uniqueName);
            var rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");
            if(fileObj!=null)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                var fileFullPath = System.IO.Path.Combine(rootPath, fileObj.uniqueName + fileObj.fileExt);

                byte[] file = System.IO.File.ReadAllBytes(fileFullPath);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(file);

                response.Content = new ByteArrayContent(file);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");

                response.Content.Headers.ContentType = new MediaTypeHeaderValue(fileObj.contentType);
                response.Content.Headers.ContentDisposition.FileName = fileObj.name;
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;

            }
        }

        [HttpGet]
        public Object getThumbnail(string uniqueName)
        {
            fileBA fileBAObj = new fileBA();
            fileDTO fileObj = fileBAObj.getFile(uniqueName);
            var rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");
            var fileFullPath = System.IO.Path.Combine(rootPath, fileObj.uniqueName + fileObj.fileExt);

            ShellFile shellFile = ShellFile.FromFilePath(fileFullPath);
            Bitmap shellThumb = shellFile.Thumbnail.MediumBitmap;

            if (fileObj != null)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                byte[] file = ImageToBytes(shellThumb);
                MemoryStream ms = new MemoryStream(file);
                response.Content = new ByteArrayContent(file);
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(fileObj.contentType);
                response.Content.Headers.ContentDisposition.FileName = fileObj.name;
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }

        }
        private byte[] ImageToBytes(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}