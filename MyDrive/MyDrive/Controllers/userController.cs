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
            int id=userObjBA.validateUser(userObj);
            if (id!=0)
            {
                Session["user"] = userObj.txtLogin;
                Session["userId"] = id;
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