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
        public void addNewFolder()
        {
            folderDTO obj = new folderDTO();
            obj.name = System.Web.HttpContext.Current.Request["name"];
            obj.parentFolderId = Convert.ToInt32(System.Web.HttpContext.Current.Request["parentFolderId"]);
            obj.createdBy = Convert.ToInt32(System.Web.HttpContext.Current.Request["createdBy"]);
            folderBA folderBAObj = new folderBA();
            folderBAObj.saveFolder(obj);
            //if (folderBAObj.saveFolder(obj))
            //return true;
            //return false;
        }

        [HttpPost]
        public List<folderDTO> loadFolders()
        {
            int createdBy = 0;
            createdBy = Convert.ToInt32(System.Web.HttpContext.Current.Request["createdBy"]);
            folderBA folderBAObj = new folderBA();
            List<folderDTO> folderList = folderBAObj.getAllFolders(createdBy);
            return folderList;
        }
	}
}