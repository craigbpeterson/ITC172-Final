using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryApp.Models
{
    public class ReceiptModel
    {
        public string MessageTitle { get; set; }
        public string MessageText { get; set; }
        public int ProductKey { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public string TransactionTotal { get; set; }
    }
}