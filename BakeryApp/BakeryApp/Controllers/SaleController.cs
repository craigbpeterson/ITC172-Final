using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BakeryApp.Models;

namespace BakeryApp.Controllers
{
    public class SaleController : Controller
    {
        BakeryEntities1 db = new BakeryEntities1();
        // GET: Sale
        public ActionResult Index()
        {
            List<SelectListItem> discount = new List<SelectListItem>();
            discount.Add(new SelectListItem
            {
                Text = "None",
                Value = "1",
                Selected = true
            });
            discount.Add(new SelectListItem
            {
                Text = "Student",
                Value = "2"
            });
            discount.Add(new SelectListItem
            {
                Text = "Senior Citizen",
                Value = "3"
            });
            discount.Add(new SelectListItem
            {
                Text = "Military",
                Value = "4"
            });

            ViewBag.DiscountType = discount;
            return View();
        }
        public ActionResult Index([Bind(Include = "ProductName, ProductPrice, ProductQuantity")]ProductSale p)
        {
           

            db.SaleDetails.Add
            

        }
    }
}