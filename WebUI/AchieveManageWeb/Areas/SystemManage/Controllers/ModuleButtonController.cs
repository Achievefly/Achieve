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
    public class ModuleButtonController : BaseController
    {
        private Sys_ModuleBLL moduleApp = new Sys_ModuleBLL();
        private Sys_ModuleButtonBLL moduleButtonApp = new Sys_ModuleButtonBLL();
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson(string moduleId)
        {
            var data = moduleButtonApp.GetList(moduleId);
            var treeList = new List<TreeSelectModel>();
            foreach (Sys_ModuleButton item in data)
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
        public ActionResult GetTreeGridJson(string moduleId, string keyword)
        {
            var data = moduleButtonApp.GetList(moduleId);
            var treeList = new List<TreeGridModel>();
            if (keyword != "" && keyword != null)
            {
                data = data.TreeWhere(c => c.F_FullName.Contains(keyword));
            }
            if (data != null)
            {
                foreach (Sys_ModuleButton item in data)
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
            }
            return Content(treeList.TreeGridJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = moduleButtonApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(Sys_ModuleButton moduleButtonEntity, string keyValue)
        {
            try
            {
                bool i = false;
                if (keyValue == "" || keyValue == null)
                {
                    moduleButtonEntity.F_CreatorUserId = OperatorProvider.Provider.GetCurrent().UserCode;
                    moduleButtonEntity.F_CreatorTime = DateTime.Now;
                    moduleButtonEntity.F_Id = System.Guid.NewGuid().ToString();
                    i = moduleButtonApp.Add(moduleButtonEntity);
                }
                else
                {
                    moduleButtonEntity.F_Id = keyValue;
                    moduleButtonEntity.F_LastModifyUserId = OperatorProvider.Provider.GetCurrent().UserCode;
                    moduleButtonEntity.F_LastModifyTime = DateTime.Now;
                    string[] notstr = { "F_CreatorUserId", "F_CreatorTime" };
                    i = moduleButtonApp.Update(moduleButtonEntity, notstr);
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
                i = moduleButtonApp.Delete(keyValue);
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
        [HttpGet]
        public ActionResult CloneButton()
        {
            return View();
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetCloneButtonTreeJson()
        {
            var moduledata = moduleApp.GetList();
            var buttondata = moduleButtonApp.GetList();
            var treeList = new List<TreeViewModel>();
            foreach (Sys_Module item in moduledata)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = moduledata.Count(t => t.F_ParentId == item.F_Id) == 0 ? false : true;
                tree.id = item.F_Id;
                tree.text = item.F_FullName;
                tree.value = item.F_EnCode;
                tree.parentId = item.F_ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = true;
                treeList.Add(tree);
            }
            foreach (Sys_ModuleButton item in buttondata)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = buttondata.Count(t => t.F_ParentId == item.F_Id) == 0 ? false : true;
                tree.id = item.F_Id;
                tree.text = item.F_FullName;
                tree.value = item.F_EnCode;
                if (item.F_ParentId == "0")
                {
                    tree.parentId = item.F_ModuleId;
                }
                else
                {
                    tree.parentId = item.F_ParentId;
                }
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.hasChildren = hasChildren;
                if (item.F_Icon != "")
                {
                    tree.img = item.F_Icon;
                }
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        //[HttpPost]
        //[HandlerAjaxOnly]
        //public ActionResult SubmitCloneButton(string moduleId, string Ids)
        //{
        //    moduleButtonApp.SubmitCloneButton(moduleId, Ids);
        //    return Success("克隆成功。");
        //}
    }
}
