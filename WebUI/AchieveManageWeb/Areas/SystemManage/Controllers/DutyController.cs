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
            try
            {
                bool i = false;
                Sys_User uInfo = ViewData["Account"] as Sys_User;
                if (keyValue == "" || keyValue == null)
                {
                    roleEntity.F_CreatorUserId = uInfo.F_Account;
                    roleEntity.F_CreatorTime = DateTime.Now;
                    roleEntity.F_Category = 2;
                    roleEntity.F_Id = System.Guid.NewGuid().ToString();
                    i = dutyApp.Add(roleEntity);
                }
                else
                {
                    roleEntity.F_Id = keyValue;
                    roleEntity.F_LastModifyUserId = uInfo.F_Account;
                    roleEntity.F_LastModifyTime = DateTime.Now;
                    string[] notstr = { "F_CreatorUserId", "F_CreatorTime", "F_Category" };
                    i = dutyApp.Update(roleEntity, notstr);
                }
                if (i)
                {
                    return Success();
                }
                else
                {
                    return Warning();
                }

            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            try
            {
                bool i = false;
                i = dutyApp.Delete(keyValue);
                if (i)
                {
                    return Success("删除成功");
                }
                else
                {
                    return Warning();
                }

            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}
