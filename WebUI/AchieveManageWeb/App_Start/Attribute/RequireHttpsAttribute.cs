using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AchieveManageWeb.App_Start.Attribute
{
    /// <summary>
    /// http重定向到https
    /// </summary>
    public class RequireHttpsAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return true;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsSecureConnection)
            {
                string UrlHost = HttpContext.Current.Request.Url.Host;
                // 获取当前请求的Path
                //string path = filterContext.HttpContext.Request.Path;
                string url = filterContext.HttpContext.Request.RawUrl;
                // 重定向到https连接
                //filterContext.HttpContext.Response.Redirect(string.Format("https://{0}{1}", UrlHost, url), true);
                filterContext.Result = new RedirectResult(string.Format("https://{0}{1}", UrlHost, url));

            }
            else
            {
                base.OnAuthorization(filterContext);
            }
          
            
        }
    }
}