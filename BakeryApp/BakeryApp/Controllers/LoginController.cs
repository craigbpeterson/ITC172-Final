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
        BakeryEntities db = new BakeryEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "PersonEmail, PersonPhone")] Person p)
        {
            int loginResult = db.usp_PersonLogin(p.PersonEmail, p.PersonPhone);

            if (loginResult != -1)
            {
                var uid = (from r in db.People
                           where r.PersonEmail.Equals(p.PersonEmail)
                           select r.PersonKey).FirstOrDefault();

                int rKey = (int)uid;
                Session["PersonKey"] = rKey;

                Message m = new Message();
                m.MessageText = "Thank you, " + p.PersonFirstName + " for loggin' in." +
                    "Feel free to browse our tasty selection.";

                return RedirectToAction("Result", m);
            }
        }
    }
}