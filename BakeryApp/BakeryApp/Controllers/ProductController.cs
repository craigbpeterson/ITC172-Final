using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BakeryApp.Models;

namespace BakeryApp.Controllers
{
    public class ProductController : Controller
    {
        private BakeryEntities1 db = new BakeryEntities1();
        // GET: Product
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public ActionResult Buy(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Session["PurchaseSelection"] = id;


            Product p = db.Products.Find(id);

            if (p == null)
            {
                return HttpNotFound();
            }
                        
            return RedirectToAction("Index", "Sale");
        }
    }
}