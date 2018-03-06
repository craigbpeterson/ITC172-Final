using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BakeryApp.Models
{
    public class LoginClass
    {  
        public LoginClass() { }

        public LoginClass(string name, string pass)
        {
            PersonEmail = name;
            PersonPhone = pass;
        }
        public string PersonEmail { get; set; }
        public string PersonPhone { get; set; }
    }
}