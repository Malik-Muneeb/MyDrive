using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyDrive.Controllers
{
    public class homeController : Controller
    {
        public ActionResult home()
        {
            if(Session["user"]!=null)
                return View();
            return Redirect("~/user/Login");
        }

    }
}