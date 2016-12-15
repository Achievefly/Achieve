using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AchieveManageWeb.App_Start;
using AchieveCommon;
using AchieveBLL;
using AchieveManageWeb.App_Start.BaseController;
using AchieveEntity;
using System;
using AchieveCommon.Operator;

namespace AchieveManageWeb.Areas.SystemManage.Controllers
{
    public class ItemsDataController : BaseController
    {
        private Sys_ItemsDetailBLL itemsDetailApp = new Sys_ItemsDetailBLL();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string itemId, string keyword)
        {
            var data = itemsDetailApp.GetList(itemId, keyword);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson(string enCode)
        {
            var data = itemsDetailApp.GetItemList(enCode);
            List<object> list = new List<object>();
            foreach (Sys_ItemsDetail item in data)
            {
                list.Add(new { id = item.F_ItemCode, text = item.F_ItemName });
            }
            return Content(list.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = itemsDetailApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(Sys_ItemsDetail itemsDetailEntity, string keyValue)
        {
            try
            {
                bool i = false;
                Sys_User uInfo = ViewData["Account"] as Sys_User;
                if (keyValue == "" || keyValue == null)
                {
                    itemsDetailEntity.F_CreatorUserId = OperatorProvider.Provider.GetCurrent().UserCode;
                    itemsDetailEntity.F_CreatorTime = DateTime.Now;
                    itemsDetailEntity.F_Id = System.Guid.NewGuid().ToString();
                    i = itemsDetailApp.Add(itemsDetailEntity);
                }
                else
                {
                    itemsDetailEntity.F_Id = keyValue;
                    itemsDetailEntity.F_LastModifyUserId = OperatorProvider.Provider.GetCurrent().UserCode;
                    itemsDetailEntity.F_LastModifyTime = DateTime.Now;
                    string[] notstr = { "F_CreatorUserId", "F_CreatorTime", "F_ParentId", "F_ItemId" };
                    i = itemsDetailApp.Update(itemsDetailEntity, notstr);
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
                i = itemsDetailApp.Delete(keyValue);
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
