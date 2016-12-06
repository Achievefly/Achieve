using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AchieveManageWeb.App_Start;
using AchieveCommon;
using AchieveBLL;
using AchieveManageWeb.App_Start.BaseController;
using AchieveEntity;
using System;

namespace AchieveManageWeb.Areas.SystemManage.Controllers
{
    public class DutyController : BaseController
    {
        private Sys_DutyBLL dutyApp = new Sys_DutyBLL();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string keyword)
        {
            var data = dutyApp.GetList();
            if (keyword != null && keyword != "")
            {
                data = data.Where(c => c.F_FullName.Contains(keyword)).ToList();
            }
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = dutyApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(Sys_Role roleEntity, string keyValue)
        {
            Sys_User uInfo = ViewData["Account"] as Sys_User;
            if (keyValue == "" || keyValue == null)
            {
                roleEntity.F_CreatorUserId = uInfo.F_Account;
                roleEntity.F_CreatorTime = DateTime.Now;
                roleEntity.F_Category = 2;
                roleEntity.F_Id = System.Guid.NewGuid().ToString();
                dutyApp.Add(roleEntity);
            }
            else
            {
                roleEntity.F_Id = keyValue;
                roleEntity.F_LastModifyUserId = uInfo.F_Account;
                roleEntity.F_LastModifyTime = DateTime.Now;
                string[] notstr = { "F_CreatorUserId", "F_CreatorTime", "F_Category" };
                dutyApp.Update(roleEntity, notstr);
            }
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            dutyApp.Delete(keyValue);
            return Success("删除成功。");
        }
    }
}
