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
            UserName = name;
            Password = pass;
        }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}