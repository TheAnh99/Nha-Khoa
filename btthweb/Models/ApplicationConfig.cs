using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace btthweb.Models
{
    public class ApplicationConfig
    {
        public const string Admin = "ADMIN";
        public const string Customer = "CUSTOMER";
        public const string username = "username";
        public const string unknown = "unknown";
        public const string AccountType = "AccountType";
        public const string UserInfo = "UserInfo";
        public const string Language = "language";
        public const string SanPham = "SanPham";
        public const int Active = 1;
        public const int InActive = 0;

        public static string DefaultPassword = ConfigurationManager.AppSettings["PasswordCreateUSER"].ToString();
    }
}