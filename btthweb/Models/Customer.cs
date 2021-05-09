using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace btthweb.Models
{
    public class Customer
    {
        public string UserName { get; set; }

        public string Phone { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        //[Display(Name = "Mã chứng khoán")]
        public int CompanyID { get; set; }

        public string Note { get; set; }

        public int Active { get; set; }

        public string Symbol { get; set; }
        public string Exchange { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNameEN { get; set; }
        public string Expert { get; set; }
        public string ExpertFullName { get; set; }
        public int RegionID { get; set; }

        public int Posted { get; set; }
        public DateTime CreateDate { get; set; }
        public string ExpertEmail { get; set; }
        public string CC { get; set; }
        public string ExpertPhone { get; set; }
    }
}