﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models.FormViewModel
{
    public class ListProductViewModel
    {
       public List<SanPham> ListProduct { get; set; }

        public List<SanPham> ListProducts { get; set; }

        public string Tensp { get; set; }
        public List<SanPham> CurrentListProductType { get; set; }
    }
}