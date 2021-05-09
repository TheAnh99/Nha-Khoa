using btthweb.Appcode.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace btthweb.Models.FormViewModel
{
    public class CreateProduct
    {
        public SanPham SanPham { get; set; }
        public List<SanPham>SP { get; set; }

        public CreateProduct()
        {
            SP = DatabaseProduct.Lay_DS_SanPham();
        }
    }
}