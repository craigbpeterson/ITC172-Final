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
        public ActionResult Index([Bind(Include = "PersonEmail, PersonPhone")]LoginClass lc)
        {
            BakeryEntities1 db = new BakeryEntities1();

            int loginResult = db.usp_Login(lc.PersonEmail, lc.PersonPhone);

            System.Diagnostics.Debug.WriteLine("PersonEmail= " + lc.PersonEmail);
            System.Diagnostics.Debug.WriteLine("PersonPhone= " + lc.PersonPhone);
            System.Diagnostics.Debug.WriteLine("loginResult= " + loginResult);

            if (loginResult != -1)
            {
                var uid = (from r in db.People
                           where r.PersonEmail.Equals(lc.PersonEmail)
                           select r.PersonKey).FirstOrDefault();

                int rKey = (int)uid;
                Session["PersonKey"] = rKey;

                Message m = new Message();
                m.MessageText = "Thank you, " + lc.PersonEmail + " for loggin' in." +
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
    
 