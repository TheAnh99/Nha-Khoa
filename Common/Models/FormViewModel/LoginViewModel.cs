using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Common.Models.FormViewModel
{
    public class LoginViewModel
    {
        public string UserName { get; set; }



        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}