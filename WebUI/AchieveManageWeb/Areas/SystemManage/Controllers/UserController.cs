using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AchieveManageWeb.App_Start;
using AchieveCommon;
using AchieveBLL;
using AchieveManageWeb.App_Start.BaseController;
using AchieveEntity;
using System;
using AchieveCommon.Web;


namespace AchieveManageWeb.Areas.SystemManage.Controllers
{
    public class UserController : BaseController
    {
        private Sys_UserBLL userApp = new Sys_UserBLL();
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            int records = 0;
            var data = new
            {
                rows = userApp.GetList(pagination, keyword,out records),
                total = records % pagination.rows == 0 ? records / pagination.rows : records / pagination.rows + 1,
                page = pagination.page,
                records = records
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = userApp.GetUserById(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(Sys_User userEntity, string keyValue)
        {
            Sys_User uInfo = ViewData["Account"] as Sys_User;
            if (keyValue == "" || keyValue == null)
            {
                userEntity.F_CreatorUserId = uInfo.F_Account;
                userEntity.F_CreatorTime = DateTime.Now;
                userEntity.F_Id = System.Guid.NewGuid().ToString();
                userApp.Add(userEntity);
            }
            else
            {
                userEntity.F_Id = keyValue;
                userEntity.F_LastModifyUserId = uInfo.F_Account;
                userEntity.F_LastModifyTime = DateTime.Now;
                string[] notstr = { "F_Password", "F_HeadIcon", "F_SecurityLevel", "F_Signature", "F_CreatorUserId", "F_CreatorTime" };
                userApp.Update(userEntity,notstr);
            }
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            userApp.Delete(keyValue);
            return Success("删除成功。");
        }
        [HttpGet]
        public ActionResult RevisePassword()
        {
            return View();
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitRevisePassword(string userPassword, string keyValue)
        {
            userApp.RevisePassword(userPassword, keyValue);
            return Success("重置密码成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DisabledAccount(string keyValue)
        {
            userApp.Startusing(keyValue, 0);
            return Success("账户禁用成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult EnabledAccount(string keyValue)
        {
            userApp.Startusing(keyValue,1);
            return Success("账户启用成功。");
        }

        [HttpGet]
        public ActionResult Info()
        {
            return View();
        }
    }
}

