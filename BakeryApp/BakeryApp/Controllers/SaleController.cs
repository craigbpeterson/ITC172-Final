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
            int id = (int)Session["PurchaseSelection"];

            Product p = db.Products.Find(id);

            ProductSale ps = new ProductSale();
            ps.ProductKey = p.ProductKey;
            ps.ProductName = p.ProductName;
            ps.ProductPrice = p.ProductPrice;
            ps.ProductQuantity = 1;

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
            return View(ps);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ProductKey, ProductName, ProductPrice, ProductQuantity, DiscountType, CustomerEatIn")]ProductSale ps)
        {
            int id = (int)Session["PurchaseSelection"];

            Product p = db.Products.Find(id);
            ps.ProductKey = p.ProductKey;
            ps.ProductName = p.ProductName;
            ps.ProductPrice = p.ProductPrice;
            
            double discountAmount;

            if (ps.DiscountType == 2)
            {
                discountAmount = 0.9;
            }
            else if (ps.DiscountType == 3)
            {
                discountAmount = 0.85;
            }
            else if (ps.DiscountType == 4)
            {
                discountAmount = 0.8;
            }
            else { discountAmount = 1; }

            double EatInTax = 0;
            if (ps.CustomerEatIn)
            {
                EatInTax = 0.25;
            }

            System.Diagnostics.Debug.WriteLine("ProductKey: " + ps.ProductKey);
            System.Diagnostics.Debug.WriteLine("ProductName: " + ps.ProductName);
            System.Diagnostics.Debug.WriteLine("ProductPrice: " + ps.ProductPrice);
            System.Diagnostics.Debug.WriteLine("ProductQuantity: " + ps.ProductQuantity);
            System.Diagnostics.Debug.WriteLine("DiscountAmount: " + discountAmount);
            System.Diagnostics.Debug.WriteLine("EatInTax: " + EatInTax);

            double purchaseSubtotal = (((double)ps.ProductPrice * ps.ProductQuantity) * discountAmount) + EatInTax;
            double salesTax = 1.0996;
            double purchaseTotal = Math.Round((purchaseSubtotal * salesTax), 2);
            
            Sale s = new Sale();
            s.SaleDate = DateTime.Now;
            s.CustomerKey = 1;
            s.EmployeeKey = 2;
            db.Sales.Add(s);

            SaleDetail anotherSale = new SaleDetail();
            anotherSale.ProductKey = ps.ProductKey;
            anotherSale.SaleDetailPriceCharged = (decimal)purchaseTotal;
            anotherSale.SaleDetailQuantity = ps.ProductQuantity;
            anotherSale.SaleDetailDiscount = (decimal)discountAmount;
            anotherSale.SaleDetailSaleTaxPercent = ((decimal)salesTax - 1);
            anotherSale.SaleDetailEatInTax = (decimal)EatInTax;
            db.SaleDetails.Add(anotherSale);

            db.SaveChanges();

            Message m = new Message();
            m.MessageTitle = "Transaction Complete";
            m.MessageText = "Thank you for your purchase!";

            return View("Receipt", m);
        }

        public ActionResult Receipt(Message m)
        {
            return View(m);
        }
    }
}