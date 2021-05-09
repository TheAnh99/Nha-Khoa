using btthweb.Appcode.DAL;
using btthweb.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace CBTT.Appcode.BLL
{
    //[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]

    public class RequiredLoginAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Kiểm tra người dùng đăng nhập và rout về đúng trang
        /// nếu chưa đăng nhập đưa về login
        /// </summary>
        /// <param name="filterContext"></param>
        ///

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (HttpContext.Current.Session[ApplicationConfig.username] == null)
                return false;
            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!this.AuthorizeCore(filterContext.HttpContext))
            {
                UrlHelper u = new UrlHelper(filterContext.Controller.ControllerContext.RequestContext);
                string url = u.Action("Index", "Login", null);
                filterContext.Result = new RedirectResult(url);
            }

            //check để di chuyển về đúng trang
            try
            {
                //Users user = (Users) HttpContext.Current.Session[ApplicationConfig.UserInfo];
                string strActionName = filterContext.Controller.ControllerContext.RouteData.Values["controller"]
                    .ToString().ToLower(); // lấy controller hiện tại
                if (HttpContext.Current.Session[ApplicationConfig.AccountType].ToString() == ApplicationConfig.Admin)
                {
                    if (strActionName == "customer" || strActionName == "home")
                    {
                        UrlHelper u = new UrlHelper(filterContext.Controller.ControllerContext.RequestContext);
                        string url = u.Action("ManageUser", "Internal", null);

                        filterContext.Result = new RedirectResult(url);
                    }
                }
                else if (HttpContext.Current.Session[ApplicationConfig.AccountType].ToString() == ApplicationConfig.Customer)
                {
                    if (strActionName == "internal" || strActionName == "home")
                    {
                        UrlHelper u = new UrlHelper(filterContext.Controller.ControllerContext.RequestContext);
                        string url = u.Action("Document", "Customer", null);
                        filterContext.Result = new RedirectResult(url);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.Error(ex.ToString());   // Ghi thông tin ra file
            }
        }
    }
}