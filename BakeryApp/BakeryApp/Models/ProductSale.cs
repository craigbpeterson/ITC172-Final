using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryApp.Models
{
    public class ProductSale
    {
        public ProductSale() { }

        public ProductSale(int Key, string Name, decimal Price, int Quantity = 1, int Discount = 1, bool EatIn = false)
        {
            ProductKey = Key;
            ProductName = Name;
            ProductPrice = Price;
            ProductQuantity = Quantity;
            DiscountType = Discount;
            CustomerEatIn = EatIn;
        }

        public int ProductKey { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public int DiscountType { get; set; }
        public bool CustomerEatIn { get; set; }
    }
}