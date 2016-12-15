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
using AchieveCommon.Web.Tree;
using AchieveCommon.Web.TreeView;
using AchieveCommon.Web.TreeGrid;
using AchieveCommon.Operator;

namespace AchieveManageWeb.Areas.SystemManage.Controllers
{
    public class ModuleController : BaseController
    {
        private Sys_ModuleBLL moduleApp = new Sys_ModuleBLL();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = moduleApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (Sys_Module item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.F_FullName;
                treeModel.parentId = item.F_ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeGridJson(string keyword)
        {
            var data = moduleApp.GetList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.F_FullName.Contains(keyword));
            }
            var treeList = new List<TreeGridModel>();
            foreach (Sys_Module item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_Id) == 0 ? false : true;
                treeModel.id = item.F_Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.F_ParentId;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = moduleApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(Sys_Module moduleEntity, string keyValue)
        {
            try
            {
                bool i = false;
                if (keyValue == "" || keyValue == null)
                {
                    moduleEntity.F_CreatorUserId = OperatorProvider.Provider.GetCurrent().UserCode;
                    moduleEntity.F_CreatorTime = DateTime.Now;
                    moduleEntity.F_Id = System.Guid.NewGuid().ToString();
                    string[] notstr = { "ChildNodes" };
                    i = moduleApp.Add(moduleEntity, notstr);
                }
                else
                {
                    moduleEntity.F_Id = keyValue;
                    moduleEntity.F_LastModifyUserId = OperatorProvider.Provider.GetCurrent().UserCode;
                    moduleEntity.F_LastModifyTime = DateTime.Now;
                    string[] notstr = { "ChildNodes", "F_CreatorUserId", "F_CreatorTime" };
                    i = moduleApp.Update(moduleEntity, notstr);
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
                i = moduleApp.Delete(keyValue);
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
