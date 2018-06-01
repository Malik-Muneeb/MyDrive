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
                return Redirect("/user/home");
            else
            {
                ViewBag.txtLogin = userObj.txtLogin;
                ViewBag.msg = "Incorrect Info";
            }
            return View();
        }
	}
}