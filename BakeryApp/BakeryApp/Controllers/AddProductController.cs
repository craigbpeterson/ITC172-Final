using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BakeryApp.Models;

namespace BakeryApp.Controllers
{
    public class AddProductController : Controller
    {
        BakeryEntities1 db = new BakeryEntities1();
        // GET: AddProduct
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ProductName, ProductPrice")] Product np)
        {
            Product newproduct = new Product();
            newproduct.ProductName = np.ProductName;
            newproduct.ProductPrice = np.ProductPrice;
            
            db.Products.Add(newproduct);
            db.SaveChanges();

            Message m = new Message();
            m.MessageTitle = "Product Added Successfully";
            m.MessageText = "Thank you for adding " + np.ProductName + " to our ever-increasing list of delicious menu options!";
            return View("Result", m);
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }
    }
}