using BAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
//using System.Web.Mvc;
using System.Net;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;


namespace Web_API.ApiControllers
{
    [EnableCors(origins: "http://localhost:1747", headers: "*", methods: "*")]
    public class folderDataController : ApiController
    {
        [HttpPost]
        public bool addNewFolder()
        {
            folderDTO obj = new folderDTO();
            obj.name = System.Web.HttpContext.Current.Request["name"];
            obj.parentFolderId = Convert.ToInt32(System.Web.HttpContext.Current.Request["parentFolderId"]);
            obj.createdBy = Convert.ToInt32(System.Web.HttpContext.Current.Request["createdBy"]);
            folderBA folderBAObj = new folderBA();
            //folderBAObj.saveFolder(obj);
            if (folderBAObj.saveFolder(obj))
                return true;
            return false;
        }

        [HttpPost]
        public List<folderDTO> loadFolders()
        {
            folderDTO obj = new folderDTO();
            obj.createdBy = Convert.ToInt32(System.Web.HttpContext.Current.Request["createdBy"]);
            obj.parentFolderId = Convert.ToInt32(System.Web.HttpContext.Current.Request["parentFolderId"]);
            folderBA folderBAObj = new folderBA();
            List<folderDTO> folderList = folderBAObj.getAllFolders(obj);
            return folderList;
        }

        [HttpPost]
        public bool deleteFolder()
        {
            int id = Convert.ToInt32(System.Web.HttpContext.Current.Request["id"]);
            folderBA folderBAObj = new folderBA();
            if (folderBAObj.deleteFolder(id))
                return true;
            return false;
        }

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
            document.Close();
            return;
        }
        
	}
}