using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AchieveManageWeb.App_Start;
using AchieveCommon;
using AchieveBLL;
using AchieveManageWeb.App_Start.BaseController;
using AchieveEntity;
using System;
using AchieveCommon.Web.Tree;
using AchieveCommon.Web.TreeGrid;
using System.Text;
using AchieveCommon.Web;

namespace AchieveManageWeb.Controllers
{
    public class ClientsDataController : BaseController
    {
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetClientsDataJson()
        {
            var data = new
            {
                dataItems = this.GetDataItemList(),
                organize = this.GetOrganizeList(),
                role = this.GetRoleList(),
                duty = this.GetDutyList(),
                user = "",
                authorizeMenu = this.GetMenuList(),
                authorizeButton = this.GetMenuButtonList(),
            };
            return Content(data.ToJson());
        }
        private object GetDataItemList()
        {
            var itemdata = new Sys_ItemsDetailBLL().GetList();
            Dictionary<string, object> dictionaryItem = new Dictionary<string, object>();
            foreach (var item in new Sys_ItemsBLL().GetList())
            {
                var dataItemList = itemdata.FindAll(t => t.F_ItemId.Equals(item.F_Id));
                Dictionary<string, string> dictionaryItemList = new Dictionary<string, string>();
                foreach (var itemList in dataItemList)
                {
                    dictionaryItemList.Add(itemList.F_ItemCode, itemList.F_ItemName);
                }
                dictionaryItem.Add(item.F_EnCode, dictionaryItemList);
            }
            return dictionaryItem;
        }
        private object GetOrganizeList()
        {
            Sys_OrganizeBLL organizeApp = new Sys_OrganizeBLL();
            var data = organizeApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (Sys_Organize item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_EnCode,
                    fullname = item.F_FullName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }
            return dictionary;
        }
        private object GetRoleList()
        {
            Sys_RoleBLL roleApp = new Sys_RoleBLL();
            var data = roleApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (Sys_Role item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_EnCode,
                    fullname = item.F_FullName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }
            return dictionary;
        }
        private object GetDutyList()
        {
            Sys_DutyBLL dutyApp = new Sys_DutyBLL();
            var data = dutyApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (Sys_Role item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_EnCode,
                    fullname = item.F_FullName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }
            return dictionary;
        }
        private object GetMenuList()
        {
            Sys_User uInfo = ViewData["Account"] as Sys_User;
            var roleId = uInfo.F_RoleId;
            return new Sys_ModuleBLL().GetMRoleList(roleId);
            
        }
        //private string ToMenuJson(List<Sys_Module> data, string parentId)
        //{
        //    StringBuilder sbJson = new StringBuilder();
        //    sbJson.Append("[");
        //    List<Sys_Module> entitys = data.FindAll(t => t.F_ParentId == parentId);
        //    if (entitys.Count > 0)
        //    {
        //        foreach (var item in entitys)
        //        {
        //            string strJson = item.ToJson();
        //            strJson = strJson.Insert(strJson.Length - 1, ",\"ChildNodes\":" + ToMenuJson(data, item.F_Id) + "");
        //            sbJson.Append(strJson + ",");
        //        }
        //        sbJson = sbJson.Remove(sbJson.Length - 1, 1);
        //    }
        //    sbJson.Append("]");
        //    return sbJson.ToString();
        //}
        /// <summary>
        /// 获取按钮
        /// </summary>
        /// <returns></returns>
        private object GetMenuButtonList()
        {
            Sys_User uInfo = ViewData["Account"] as Sys_User;
            var roleId = uInfo.F_RoleId;
            var data = new Sys_ModuleButtonBLL().GetButtonList(roleId);
            
            var dataModuleId = data.Distinct(new ExtList<Sys_ModuleButton>("F_ModuleId"));
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (Sys_ModuleButton item in dataModuleId)
            {
                var buttonList = data.Where(t => t.F_ModuleId.Equals(item.F_ModuleId));
                dictionary.Add(item.F_ModuleId, buttonList);
            }
            return dictionary;
        }
    }
}
