using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class Users
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
        public string CC { get; set; }

        public string Phone { get; set; }

        public int RegionID { get; set; }
        public string Region { get; set; }

        public string Note { get; set; }

        public int Active { get; set; }

        //        public string Exchange { get; set; }

        public DateTime CreateDate { get; set; }
    }
}