using AchieveCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AchieveCommon.Operator;

namespace AchieveManageWeb.App_Start
{
    /// <summary>
    /// 判断是否登录的过滤器
    /// </summary>
    public class JudgmentLoginAttribute : ActionFilterAttribute
    {
        public AchieveEntity.Sys_User accountmodelJudgment;

        //执行Action之前操作
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            //判断是否登录或是否用权限，如果有那么就进行相应的操作，否则跳转到登录页或者授权页面
            string s_accountId = OperatorProvider.Provider.GetCurrent().UserId;
          
            //判断是否有cookie
            if (s_accountId!=null)
            {
                AchieveEntity.Sys_User m_account = new AchieveBLL.Sys_UserBLL().GetUserById(s_accountId);
                if (m_account != null)
                {
                    //accountmodelJudgment = m_account;
                    //filterContext.Controller.ViewData["Account"] = m_account;
                    //filterContext.Controller.ViewData["AccountName"] = m_account.F_Account;
                    //filterContext.Controller.ViewData["RealName"] = m_account.F_RealName;

                    //处理Action之前操作内容根据我们提供的规则来定义这部分内容
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    //CookiesHelper.AddCookie("UserID", System.DateTime.Now.AddDays(-1));
                    filterContext.Result = new RedirectResult("/Login/Index");
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("/Login/Index");
            }
        }
    }
}