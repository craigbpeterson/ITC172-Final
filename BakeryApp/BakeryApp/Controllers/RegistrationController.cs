using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BakeryApp.Models;

namespace BakeryApp.Controllers
{
    public class RegistrationController : Controller
    {
        BakeryEntities db = new BakeryEntities();
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "PersonLastName, PersonFirstName, PersonEmail, PersonPhone, PersonDateAdded")] Person dudemanbro)
        {
            Person p = new Person();
            p.PersonLastName = dudemanbro.PersonLastName;
            p.PersonFirstName = dudemanbro.PersonFirstName;
            p.PersonEmail = dudemanbro.PersonEmail;
            p.PersonPhone = dudemanbro.PersonPhone;
            p.PersonDateAdded = DateTime.Now;

            db.People.Add(p);
            db.SaveChanges();

            Message m = new Message();
            m.MessageText = "Thanks for Registering.";
            return View("Result", m);
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }

    }
}