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
	}
}