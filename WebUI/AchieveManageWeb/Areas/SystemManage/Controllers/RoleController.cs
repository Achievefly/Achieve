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
    public class RoleController : BaseController
    {
        private Sys_RoleBLL roleApp = new Sys_RoleBLL();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string keyword)
        {
            var data = roleApp.GetList(keyword);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = roleApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(Sys_Role roleEntity, string permissionIds, string keyValue)
        {
            //roleApp.SubmitForm(roleEntity, permissionIds.Split(','), keyValue);
            Sys_User uInfo = ViewData["Account"] as Sys_User;
            if (keyValue == "" || keyValue == null)
            {
                roleEntity.F_CreatorUserId = uInfo.F_Account;
                roleEntity.F_CreatorTime = DateTime.Now;
                roleEntity.F_Category = 1;
                roleEntity.F_Id = System.Guid.NewGuid().ToString();
                roleApp.Add(roleEntity, permissionIds.Split(','));
            }
            else
            {
                roleEntity.F_Id = keyValue;
                roleEntity.F_LastModifyUserId = uInfo.F_Account;
                roleEntity.F_LastModifyTime = DateTime.Now;
                roleApp.Add(roleEntity, permissionIds.Split(','),false);
            }
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            roleApp.Delete(keyValue);
            return Success("删除成功。");
        }
    }
}
