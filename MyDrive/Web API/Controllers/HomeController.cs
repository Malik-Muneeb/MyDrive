using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;
using BAL;

namespace Web_API.Controllers
{
    public class HomeController : Controller
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
    }
}
