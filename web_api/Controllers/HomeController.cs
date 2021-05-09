using Common.Models.FormViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web_api.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(ListProductViewModel listProductViewModel)
        {
            listProductViewModel.ListProduct  = DatabaseProduct.Lay_DS_SanPham();
            return View(listProductViewModel);

        }
        public ActionResult ListProduct(ListProductViewModel listProductViewModel)
        {
            listProductViewModel.ListProduct = DatabaseProduct.Lay_DS_SanPham();
            return View(listProductViewModel);

        }
    }
}