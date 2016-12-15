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
using AchieveCommon.Operator;

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
            try
            {
                bool i = false;
                if (keyValue == "" || keyValue == null)
                {
                    roleEntity.F_CreatorUserId = OperatorProvider.Provider.GetCurrent().UserCode;
                    roleEntity.F_CreatorTime = DateTime.Now;
                    roleEntity.F_Category = 1;
                    roleEntity.F_Id = System.Guid.NewGuid().ToString();
                    i = roleApp.Add(roleEntity, permissionIds.Split(','));
                }
                else
                {
                    roleEntity.F_Id = keyValue;
                    roleEntity.F_LastModifyUserId = OperatorProvider.Provider.GetCurrent().UserCode;
                    roleEntity.F_LastModifyTime = DateTime.Now;
                    i = roleApp.Add(roleEntity, permissionIds.Split(','), false);
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
                i = roleApp.Delete(keyValue);
                if (i)
                {
                    return Success("删除成功。");
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
