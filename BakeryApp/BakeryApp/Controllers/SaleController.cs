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

            //select product based on id passed from product controller
            Product p = db.Products.Find(id);

            //set up defaults for product sale view
            ProductSale ps = new ProductSale();
            ps.ProductKey = p.ProductKey;
            ps.ProductName = p.ProductName;
            ps.ProductPrice = p.ProductPrice;
            ps.ProductQuantity = 1;
            ps.CustomerEatIn = false;

            //create a list for a dropdown list of possible discounts
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

            //create a list for a dropdown of possible customers making a purchase
            ViewBag.CustomerSelect = new SelectList(db.People, "PersonKey", "PersonEmail");

            //create a list for a dropdown of possible employees facilitating the transaction
            List<SelectListItem> emp = new List<SelectListItem>();
            emp.Add(new SelectListItem
            {
                Text = "ml@gmail.com",
                Value = "1"
            });
            emp.Add(new SelectListItem
            {
                Text = "tabathaj@gmail.com",
                Value = "2"
            });
            emp.Add(new SelectListItem
            {
                Text = "lewis@gmail.com",
                Value = "3"
            });

            ViewBag.EmployeeSelect = emp;

            return View(ps);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ProductKey, ProductName, ProductPrice, ProductQuantity, DiscountType, CustomerEatIn, CustomerKey, EmployeeKey")]ProductSale ps)
        {

            //get id from user selection on product page
            int id = (int)Session["PurchaseSelection"];

            //set some product information based on the product id
            Product p = db.Products.Find(id);
            ps.ProductKey = p.ProductKey;
            ps.ProductName = p.ProductName;
            ps.ProductPrice = p.ProductPrice;
            

            //set discount amount based on user selection
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

            //print to console to make sure values are being saved properly
            System.Diagnostics.Debug.WriteLine("ProductKey: " + ps.ProductKey);
            System.Diagnostics.Debug.WriteLine("ProductName: " + ps.ProductName);
            System.Diagnostics.Debug.WriteLine("ProductPrice: " + ps.ProductPrice);
            System.Diagnostics.Debug.WriteLine("ProductQuantity: " + ps.ProductQuantity);
            System.Diagnostics.Debug.WriteLine("DiscountAmount: " + discountAmount);
            System.Diagnostics.Debug.WriteLine("EatInTax: " + EatInTax);
            System.Diagnostics.Debug.WriteLine("CustomerKey: " + ps.CustomerKey);
            System.Diagnostics.Debug.WriteLine("EmployeeKey: " + ps.EmployeeKey);

            //calculate subtotal and total after taxes
            double purchaseSubtotal = (((double)ps.ProductPrice * ps.ProductQuantity) * discountAmount) + EatInTax;
            double salesTax = 1.0996;
            double purchaseTotal = Math.Round((purchaseSubtotal * salesTax), 2);
            
            //stage a new record for the Sale table in the Bakery db
            Sale s = new Sale();
            s.SaleDate = DateTime.Now;
            s.CustomerKey = ps.CustomerKey;
            s.EmployeeKey = ps.EmployeeKey;
            db.Sales.Add(s);

            //stage a new record for the SaleDetail table in the Bakery db
            SaleDetail anotherSale = new SaleDetail();
            anotherSale.ProductKey = ps.ProductKey;
            anotherSale.SaleDetailPriceCharged = (decimal)purchaseTotal;
            anotherSale.SaleDetailQuantity = ps.ProductQuantity;
            anotherSale.SaleDetailDiscount = (decimal)discountAmount;
            anotherSale.SaleDetailSaleTaxPercent = ((decimal)salesTax - 1);
            anotherSale.SaleDetailEatInTax = (decimal)EatInTax;
            anotherSale.SaleKey = s.SaleKey;
            db.SaleDetails.Add(anotherSale);

            //save Sale and SaleDetail records to the Bakery db
            db.SaveChanges();
            
            //gather info to pass to receipt view
            ReceiptModel rm = new ReceiptModel();
            rm.MessageTitle = "Receipt";
            rm.MessageText = "Transaction complete. Thank you for your purchase!";
            rm.ProductKey = ps.ProductKey;
            rm.ProductName = ps.ProductName;
            rm.ProductQuantity = ps.ProductQuantity;
            rm.TransactionTotal = purchaseTotal.ToString("C"); //this converts a number to a string formatted for currency

            return View("Receipt", rm);

        }

        public ActionResult Receipt(Message m)
        {
            return View(m);
        }
    }
}