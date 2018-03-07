using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryApp.Models
{
    public class ProductSale
    {
        public ProductSale() { }

        public ProductSale(string PName, decimal price, int quantity = 1)
        {
            ProductName = PName;
            ProductPrice = price;
            ProductQuantity = quantity;
        }

        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public bool EatIn { get; set; }
    }
}