using AchieveBLL;
using AchieveCommon;
using AchieveEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AchieveCommon.Web;
using AchieveCommon.Operator;
using AchieveCommon.Net;

namespace AchieveManageWeb.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAuthCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }
        /// <summary>
        /// 处理登录的信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="CookieExpires">cookie有效期</param>
        /// <returns></returns>
        public ActionResult CheckUserLogin(Sys_User userInfo, string CookieExpires,string code)
        {
            try
            {
                if (string.IsNullOrEmpty(Session["nfine_session_verifycode"].ToString()) || Md5.md5(code.ToLower(), 16) != Session["nfine_session_verifycode"].ToString())
                {
                    throw new Exception("验证码错误，请重新输入");
                }
                AchieveEntity.Sys_User currentUser = new Sys_UserBLL().UserLogin(userInfo.F_Account, userInfo.F_Password);
                if (currentUser != null)
                {
                    if (currentUser.F_EnabledMark == false)
                    {
                        //return Content("");
                        return Json(new AjaxResult { state = ResultType.warning.ToString(), message = "用户已被禁用，请您联系管理员" });
                    }

                    //记录登录cookie
                    //CookiesHelper.SetCookie("UserID", AES.EncryptStr(currentUser.F_Id.ToString()));
                    //CookiesHelper.SetCookie("UserAccount", AES.EncryptStr(currentUser.F_Account.ToString()));
                    //CookiesHelper.SetCookie("UserName", AES.EncryptStr(currentUser.F_RealName.ToString()));
                    //CookiesHelper.SetCookie("UserRoleId", AES.EncryptStr(currentUser.F_RoleId.ToString()));
                    
                    OperatorModel operatorModel = new OperatorModel();
                    operatorModel.UserId = currentUser.F_Id;
                    operatorModel.UserCode = currentUser.F_Account;
                    operatorModel.UserName = currentUser.F_RealName;
                    operatorModel.CompanyId = currentUser.F_OrganizeId;
                    operatorModel.DepartmentId = currentUser.F_DepartmentId;
                    operatorModel.RoleId = currentUser.F_RoleId;
                    operatorModel.LoginIPAddress = Net.Ip;
                    operatorModel.LoginIPAddressName = Net.GetLocation(operatorModel.LoginIPAddress);
                    operatorModel.LoginTime = DateTime.Now;
                    //operatorModel.LoginToken = DESEncrypt.Encrypt(Guid.NewGuid().ToString());
                    if (currentUser.F_Account == "admin")
                    {
                        operatorModel.IsSystem = true;
                    }
                    else
                    {
                        operatorModel.IsSystem = false;
                    }
                    OperatorProvider.Provider.AddCurrent(operatorModel);

                    return Json(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" });
                }
                else
                {
                    return Json(new AjaxResult { state = ResultType.warning.ToString(), message = "用户名或密码错误。" });
                }
            }
            catch (Exception ex)
            {
                return Json( new AjaxResult { state = ResultType.error.ToString(), message = ex.Message });
            }
        }

        public ActionResult UserLoginOut()
        {
            //清空cookie
            CookiesHelper.AddCookie("UserID", System.DateTime.Now.AddDays(-1));
            Session.Abandon();
            Session.Clear();
            ViewData["Account"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}
