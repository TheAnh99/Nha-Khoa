using btthweb.Appcode.DAL;
using btthweb.AppCode;
using btthweb.Models.FormViewModel;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace btthweb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(ListProductViewModel listProductViewModel)
        {
            listProductViewModel.ListProduct = DatabaseProduct.Lay_DS_SanPham();
            return View(listProductViewModel);
            //CCallApi.GetTemplateAsync(CConfig.ListProduct + "?tensp=aaaa");
            //return View();
        }
        public ActionResult ListProduct(ListProductViewModel listProductViewModel)
        {
            listProductViewModel.ListProduct = DatabaseProduct.Lay_DS_SanPham();
            return View(listProductViewModel);

        }
    }
}