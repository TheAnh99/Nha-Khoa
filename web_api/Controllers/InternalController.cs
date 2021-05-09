
using Common;
using Common.Models;
using Common.Models.FormViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    public class InternalController : Controller
    {



        // GET: Internal

       // [Route(CConfig.ListProduct)]
        //public ActionResult CreateProduct()
        //    {
        //        try
        //        {
        //            var viewModel = new CreateProduct()
        //            {
        //                SanPham = new SanPham(),
        //                SP = DatabaseProduct.Lay_DS_SanPham()
        //            };
        //            return View(viewModel);
        //        }
        //        catch (Exception ex)
        //        {
        //            LogFile.Error(ex.ToString());   // Ghi thông tin ra file
        //        }
        //        return View();
        //    }

        //    [ValidateAntiForgeryToken]
        //    [HttpPost]
        //    [Route("tao-san-pham")]
        //    public ActionResult CreateProduct(CreateProduct viewModel)
        //    {
        //        try
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //            //ModelState.AddModelError("", Resources.Resource.CreateSPFail);

        //            return View("CreateProduct", viewModel);
        //            }
        //            if (string.IsNullOrEmpty(viewModel.SanPham.Tensp))
        //            {
        //            //ModelState.AddModelError("SanPham.Tensp", Resources.Resource.TenspCannotBeBlank);
        //            return View("CreateProduct", viewModel);
        //            }

        //            if (double.IsNaN(viewModel.SanPham.Gia))
        //            {
        //            //ModelState.AddModelError("SanPham.Gia", Resources.Resource.GiaCannotBeBlank);
        //            return View("CreateProduct", viewModel);
        //            }

        //            if (string.IsNullOrEmpty(viewModel.SanPham.MoTaSP))
        //            {
        //            //ModelState.AddModelError("SanPham.MoTaSP", Resources.Resource.MoTaSPCannotBeBlank);
        //            return View("CreateProduct", viewModel);
        //            }

        //        if (string.IsNullOrEmpty(viewModel.SanPham.Anh))
        //        {
        //            //ModelState.AddModelError("SanPham.Anh", Resources.Resource.AnhCannotBeBlank);
        //            return View("CreateProduct", viewModel);
        //        }

        //        string Result = DatabaseInternal.Insert_Product(viewModel.SanPham);

        //            if (Result == "Sản phẩm bị trùng.Xin mời nhập lại.")
        //            {
 
        //            //ModelState.AddModelError("", Resources.Resource.SanPhamAlreadyExists);

        //            return View("Creat", viewModel);
        //            }
        //            else if (Result == "Create Product Failed")
        //            {
 
        //            //ModelState.AddModelError("", Resources.Resource.CreateSPFail);

        //            return View("CreateProduct", viewModel);
        //            }

  
                    
        //            return RedirectToAction("Index", "Home");
        //    }
        //        catch (Exception ex)
        //        {
        //            LogFile.Error(ex.ToString());   // Ghi thông tin ra file
        //        }
        //        return RedirectToAction("Index", "Home");
        //}

        //[Route("sua-thong-tin-san-pham")]
        //public ActionResult EditProduct(int ID)
        //{
        //    try
        //    {
        //        var viewModel = new CreateProduct()
        //        {
        //            SanPham = DatabaseProduct.GetProductInfo(ID)
        //        };

        //        return View(viewModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogFile.Error(ex.ToString());   // Ghi thông tin ra file
        //    }
        //    return View();
        //}

        //[ValidateAntiForgeryToken]
        //[Route("cap-nhat-san-pham")]
        //public ActionResult UpdateProduct(CreateProduct viewModel)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            //ModelState.AddModelError("", Resources.Resource.UpdateFail);
        //            //     viewmodel.Companies = Database.GetListCompany();
        //            viewModel.SP = DatabaseProduct.Lay_DS_SanPham();
        //            return View("EditUser", viewModel);
        //        }
        //        if (string.IsNullOrEmpty(viewModel.SanPham.Tensp))
        //        {
        //            //ModelState.AddModelError("SanPham.Tensp", Resources.Resource.TenspCannotBeBlank);
        //            return View("EditUser", viewModel);
        //        }

        //        if (double.IsNaN(viewModel.SanPham.Gia))
        //        {
        //            //ModelState.AddModelError("SanPham.Gia", Resources.Resource.GiaCannotBeBlank);
        //            return View("EditUser", viewModel);
        //        }

        //        if (string.IsNullOrEmpty(viewModel.SanPham.MoTaSP))
        //        {
        //            //ModelState.AddModelError("SanPham.MoTaSP", Resources.Resource.MoTaSPCannotBeBlank);
        //            return View("EditUser", viewModel);
        //        }

        //        if (string.IsNullOrEmpty(viewModel.SanPham.Anh))
        //        {
        //            //ModelState.AddModelError("SanPham.Anh", Resources.Resource.AnhCannotBeBlank);
        //            return View("EditUser", viewModel);
        //        }

        //        if (DatabaseInternal.UpdateProductInfo(viewModel.SanPham))
        //        {
                    
        //            return RedirectToAction("Index", "Home");
        //        }
                
        //    }
        //    catch (Exception ex)
        //    {
        //        LogFile.Error(ex.ToString());   // Ghi thông tin ra file
        //    }
        //    //ModelState.AddModelError("", Resources.Resource.UpdateFail);
        //    return View("EditUser", viewModel);
        //}


        //[Route("xoa-san-pham")]
        //public ActionResult DeleteProduct(int ID)
        //{
        //    try
        //    {
               
        //        if (!DatabaseInternal.DeleteProduct(ID))
        //        {
                 
        //        }
               
        //    }
        //    catch (Exception ex)
        //    {
        //        LogFile.Error(ex.ToString());   // Ghi thông tin ra file
        //    }
        //    return RedirectToAction("Index", "Home");
        //}


       // [Route("tim-kiem-san-pham")]
        [HttpGet]

        [Route(CConfig.ListProduct)]
        public JToken FindProduct(string Tensp)
        {
            try
            {
                
                //viewModel.ListProducts = DatabaseInternal.GetListProduct(new ListProductViewModel());
                //viewModel.CurrentListProductType = DatabaseInternal.GetListProduct(viewModel);
                return JToken.FromObject(0);
            }
            catch (Exception ex)
            {
                /*LogFile.Error(ex.ToString());   */// Ghi thông tin ra file
            }
            return null;
        }

        //[Route("tao-nguoi-dung")]
        //public ActionResult CreateUser()
        //{
        //    try
        //    {
        //        var viewModel = new CreateUserViewModel()
        //        {
        //            Users = new Users(),
        //            //   Companies = Database.GetListCompany()
        //            //Regions = DatabaseProduct.GetListRegion()
        //        };
        //        return View(viewModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogFile.Error(ex.ToString());   // Ghi thông tin ra file
        //    }
        //    return View();
        //}

        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //[Route("tao-nguoi-dung")]
        //public ActionResult CreateUser(CreateUserViewModel viewModel)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            //ModelState.AddModelError("", Resources.Resource.CreateUserFail);

        //            return View("CreateUser", viewModel);
        //        }
        //        if (string.IsNullOrEmpty(viewModel.Users.UserName))
        //        {
        //            //ModelState.AddModelError("Users.UserName", Resources.Resource.UsernameCannotBeBlank);
        //            return View("CreateUser", viewModel);
        //        }

        //        if (string.IsNullOrEmpty(viewModel.Users.FullName))
        //        {
        //            //ModelState.AddModelError("Users.FullName", Resources.Resource.FullnameCannotBeBlank);
        //            return View("CreateUser", viewModel);
        //        }

        //        if (string.IsNullOrEmpty(viewModel.Users.Email))
        //        {
        //            //ModelState.AddModelError("Users.Email", Resources.Resource.EmailCannotBeBlank);
        //            return View("CreateUser", viewModel);
        //        }
        //        Regex regexEmail = new Regex(@"(?i)\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}\b");
        //        if (!regexEmail.IsMatch(viewModel.Users.Email))
        //        {
        //            //ModelState.AddModelError("Users.Email", Resources.Resource.EmailInvalid);
        //            return View("CreateUser", viewModel);
        //        }
        //        if (string.IsNullOrEmpty(viewModel.Users.Phone))
        //        {
        //            //ModelState.AddModelError("Users.Phone", Resources.Resource.PhoneCannotBeBlank);
        //            return View("CreateUser", viewModel);
        //        }
        //        //Regex regexPhone = new Regex(@"(09|03[2|6|8|9])+([0-9]{8})\b");
        //        //if (!regexPhone.IsMatch(viewModel.Users.Phone))
        //        //{
        //        //    ModelState.AddModelError("Users.Phone", Resources.Resource.PhoneInvalid);
        //        //    return View("CreateUser", viewModel);
        //        //}
        //        //if (string.IsNullOrEmpty(viewModel.Users.Note))
        //        //{
        //        //    ModelState.AddModelError("Users.Note", Resources.Resource.NoteCannotBeBlank);
        //        //    return View("CreateUser", viewModel);
        //        //}

        //        viewModel.Users.Password = ApplicationConfig.DefaultPassword;
        //        //viewmodel.Users.CompanyID = 196; // nhân viên mặc định mã chứng khoán là FTS

        //        //if (/*viewModel/*.IsActive*/*/)
        //        //{
        //        //    viewModel.Users.Active = ApplicationConfig.Active;
        //        //}
        //        //else
        //        //    viewModel.Users.Active = ApplicationConfig.InActive;

        //        //SystemLog log = new SystemLog()
        //        //{
        //        //    Action = "Tạo người dùng",
        //        //    Username = ((Users)Session[ApplicationConfig.UserInfo]).UserName,
        //        //    ErrorCode = "CREATEU00",
        //        //    MSG = ": " + viewModel.Users.UserName
        //        //};
        //        string Result = DatabaseInternal.Insert_User(viewModel.Users);

        //        if (Result == "Username bị trùng.Xin mời nhập lại.")
        //        {
        //            //log.ErrorCode = "CREATEU02";
        //            //Database.InsertLog(log);
        //            //ModelState.AddModelError("", Resources.Resource.AccountAlreadyExists);

        //            return View("CreateUser", viewModel);
        //        }
        //        else if (Result == "Create User Failed")
        //        {
        //            //log.ErrorCode = "CREATEU01";
        //            //Database.InsertLog(log);
        //            //ModelState.AddModelError("", Resources.Resource.CreateUserFail);

        //            return View("CreateUser", viewModel);
        //        }

        //        //Database.InsertLog(log);
        //        return View("CreateUser", viewModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogFile.Error(ex.ToString());   // Ghi thông tin ra file
        //    }
        //    return View("CreateUser", viewModel);
        //}
    }
}
