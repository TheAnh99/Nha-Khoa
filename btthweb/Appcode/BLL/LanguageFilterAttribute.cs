using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;

namespace CBTT.Appcode.BLL
{
    /// <summary>
    /// Chọn đúng ngôn ngữ của người dùng để hiển thị
    /// 
    /// </summary>
    public class LanguageFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var culture = "vi-VN";
            HttpCookie httpCookie = new HttpCookie("language");
            if (filterContext.HttpContext.Request.Cookies["language"] != null)
            {
                httpCookie = filterContext.HttpContext.Request.Cookies.Get("language");
                culture = filterContext.HttpContext.Request.Cookies["language"].Value;
            }
            else
            {
                HttpCookie language = new HttpCookie("language");
                language.Value = culture;
                language.Expires = DateTime.Now.AddDays(2);
                filterContext.HttpContext.Response.Cookies.Add(language);
            }


            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentUICulture =
                System.Threading.Thread.CurrentThread.CurrentCulture;
            filterContext.HttpContext.Session["language"] = culture;
        }
    }
}