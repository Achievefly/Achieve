using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AchieveManageWeb.App_Start
{
    public class BaseHandleErrorAttribute : HandleErrorAttribute, IExceptionFilter
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            //获取异常信息，入库保存 
            Exception Error = filterContext.Exception;
            string Message = Error.Message;//错误信息 
            string Url = HttpContext.Current.Request.RawUrl;//错误发生地址 
            filterContext.ExceptionHandled = true;
            int StatusCode = filterContext.RequestContext.HttpContext.Response.StatusCode;
            AchieveCommon.WriteLog.WriteErorrLog("WebError", Error);
        }
    }
}