using btthweb.Appcode.DAL;
using btthweb.Models;
using btthweb.Models.FormViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace btthweb.Controllers
{
    [RoutePrefix("")]
    public class LoginController : Controller
    {
        public LoginController()
        {
        }

        [Route("dang-nhap")]
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("dang-nhap")]
        async public Task<ActionResult> Login(LoginViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Login", viewModel);
                }

                //if (string.IsNullOrEmpty(viewModel.UserName))
                //{
                //    ModelState.AddModelError("", Resources.Resource.UsernameCannotBeBlank);
                //}
                //if (string.IsNullOrEmpty(viewModel.Password))
                //{
                //    ModelState.AddModelError("", Resources.Resource.PasswordCannotBeBlank);
                //}


                string result = DatabaseLogin.User_Login(viewModel);
                if (result == "Tài khoản đã bị vô hiệu hóa")
                {
                   // ModelState.AddModelError("", Resources.Resource.AccountIsNotActived);
                    return View("Login", viewModel);
                }
                else if (result == "Đăng nhập thành công Admin")
                {
                    System.Web.HttpContext.Current.Session[ApplicationConfig.AccountType] = ApplicationConfig.Admin;
                    System.Web.HttpContext.Current.Session[ApplicationConfig.Language] = "VN";
                    System.Web.HttpContext.Current.Session[ApplicationConfig.username] = viewModel.UserName;
                    System.Web.HttpContext.Current.Session[ApplicationConfig.UserInfo] = (Users)DatabaseLogin.GetUser(viewModel); // lấy toàn bộ thông tin
                }
                else if (result == "Đăng nhập thành công Customer")
                {
                    System.Web.HttpContext.Current.Session[ApplicationConfig.AccountType] = ApplicationConfig.Customer;
                    System.Web.HttpContext.Current.Session[ApplicationConfig.Language] = "VN";
                    System.Web.HttpContext.Current.Session[ApplicationConfig.username] = viewModel.UserName;
                    System.Web.HttpContext.Current.Session[ApplicationConfig.UserInfo] = (Customer)DatabaseLogin.GetUser(viewModel);
                }
                else if (result == "Xin Mời Nhập Lại")
                {
                   // ModelState.AddModelError("", Resources.Resource.UsernameOrPassswordIncorect);
                    return View("Login", viewModel);
                }
                else
                {
                  //  ModelState.AddModelError("", Resources.Resource.LoginFail);
                    return View("Login", viewModel);
                }
                Session[ApplicationConfig.username] = viewModel.UserName;
                //nếu đăng nhập thành công
                if (Session[ApplicationConfig.AccountType] == ApplicationConfig.Admin)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("UploadDocument", "Customer");
                }
            }
            catch (Exception ex)
            {
                LogFile.Error(ex.ToString());   // Ghi thông tin ra file
                return View("Login", viewModel);
            }
        }
    }
}