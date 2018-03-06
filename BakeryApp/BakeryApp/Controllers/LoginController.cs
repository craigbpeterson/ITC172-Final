using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BakeryApp.Models;

namespace BakeryApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "UserName, Password")] LoginClass p)
        {
            BakeryEntities db = new BakeryEntities();

            int loginResult = db.usp_PersonLogin(p.UserName, p.Password);

            if (loginResult != -1)
            {
                var uid = (from r in db.People
                           where r.PersonEmail.Equals(p.UserName)
                           select r.PersonKey).FirstOrDefault();

                int rKey = (int)uid;
                Session["PersonKey"] = rKey;

                Message m = new Message();
                m.MessageText = "Thank you, " + p.UserName + " for loggin' in." +
                    "Feel free to browse our tasty selection.";
                m.MessageTitle = "Login Successful.";

                return RedirectToAction("Result", m);
            }

            Message msg = new Message();
            msg.MessageText = "Invalid credentials. Please try again.";
            msg.MessageTitle = "Login Failure";
            
            
            return View("Result", msg);
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }
    }
}
    
 