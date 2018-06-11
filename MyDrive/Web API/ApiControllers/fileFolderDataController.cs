using BAL;
using Entities;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Web_API.ApiControllers
{
    [EnableCors(origins: "http://localhost:1747", headers: "*", methods: "*")]

    public class fileFolderDataController : ApiController
    {
        [HttpPost]
        public void getMetaData()
        {
            folderDTO obj = new folderDTO();
            obj.createdBy = Convert.ToInt32(System.Web.HttpContext.Current.Request["createdBy"]);
            obj.parentFolderId = Convert.ToInt32(System.Web.HttpContext.Current.Request["parentFolderId"]);
            folderBA folderBAObj = new folderBA();
            List<folderDTO> folderList = folderBAObj.getAllFolders(obj);

            String dest = "C:\\Users\\Muneeb\\Documents\\Visual Studio 2013\\Projects\\ead_s18_a8_bcsf15m030\\MyDrive\\Web API\\UploadedFiles\\metaInformation.pdf";
            var writer = new PdfWriter(dest);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);
            int count = folderList.Count;
            Queue<folderDTO> foldersQueue = new Queue<folderDTO>();
            for (int i = 0; i < count; i++)
                foldersQueue.Enqueue(folderList[i]);

            fileDTO fileObj = new fileDTO();
            fileObj.createdBy = Convert.ToInt32(System.Web.HttpContext.Current.Request["createdBy"]);
            fileObj.parentFolderId = Convert.ToInt32(System.Web.HttpContext.Current.Request["parentFolderId"]);
            fileBA fileBAObj = new fileBA();
            List<fileDTO> fileList = fileBAObj.getAllFiles(fileObj);

            count = fileList.Count;
            Queue<fileDTO> filesQueue = new Queue<fileDTO>();
            for (int i = 0; i < count; i++)
                filesQueue.Enqueue(fileList[i]);

            while (foldersQueue.Count > 0)
            {
                folderDTO folder = foldersQueue.Dequeue();
                document.Add(new Paragraph("Name: " + folder.name));
                document.Add(new Paragraph("Type: Folder"));
                document.Add(new Paragraph("Size: None"));
                if (folder.parentFolderId == 0)
                    document.Add(new Paragraph("Parent: Root"));
                else
                {
                    folderDTO folder1 = folderBAObj.getFolder(folder);
                    document.Add(new Paragraph("Parent: " + folder1.name));
                }
                document.Add(new Paragraph(""));
                folder.parentFolderId = folder.id;
                folderList = folderBAObj.getAllFolders(folder);
                count = folderList.Count;
                for (int i = 0; i < count; i++)
                    foldersQueue.Enqueue(folderList[i]);

                fileList = fileBAObj.getAllFilesById(folder.parentFolderId);
                count = fileList.Count;
                for (int i = 0; i < count; i++)
                    filesQueue.Enqueue(fileList[i]);
            }



            while (filesQueue.Count > 0)
            {
                fileDTO file = filesQueue.Dequeue();
                document.Add(new Paragraph("Name: " + file.name));
                document.Add(new Paragraph("Type: File"));
                document.Add(new Paragraph("Size: " + file.fileSizeinKb + " KB"));
                if (file.parentFolderId == 0)
                    document.Add(new Paragraph("Parent: Root"));
                else
                {
                    folderDTO folder1 = folderBAObj.getFolderById(file.parentFolderId);
                    document.Add(new Paragraph("Parent: " + folder1.name));
                }
                document.Add(new Paragraph(""));
            }
            document.Add(new Paragraph(""));
            document.Close();
            return;
        }

        [HttpGet]
        public HttpResponseMessage downloadMeta()
        {
            var rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");            
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            var fileFullPath = System.IO.Path.Combine(rootPath, "metaInformation.pdf");

            byte[] file = System.IO.File.ReadAllBytes(fileFullPath);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(file);

            response.Content = new ByteArrayContent(file);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition.FileName = "Meta Information.pdf";
            return response;
        }
    }
}
