using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;
using BAL;

namespace MyDrive.Controllers
{
    public class userController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(userDTO userObj)
        {
            userBA userObjBA = new userBA();
            if (userObjBA.validateUser(userObj))
            {
                Session["user"] = userObj.txtLogin;
                return Redirect("/home/home");
            }
            else
            {
                ViewBag.txtLogin = userObj.txtLogin;
                ViewBag.msg = "Incorrect Info";
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return View("Login");
        }
	}
}