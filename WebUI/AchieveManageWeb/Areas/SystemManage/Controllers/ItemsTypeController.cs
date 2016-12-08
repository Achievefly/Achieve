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

namespace AchieveManageWeb.Areas.SystemManage.Controllers
{
    public class ItemsTypeController : BaseController
    {
        private Sys_ItemsBLL itemsApp = new Sys_ItemsBLL();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = itemsApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (Sys_Items item in data)
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
        public ActionResult GetTreeJson()
        {
            var data = itemsApp.GetList();
            var treeList = new List<TreeViewModel>();
            foreach (Sys_Items item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_Id) == 0 ? false : true;
                tree.id = item.F_Id;
                tree.text = item.F_FullName;
                tree.value = item.F_EnCode;
                tree.parentId = item.F_ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeGridJson(string keyword = "")
        {
            var data = itemsApp.GetList();
            var treeList = new List<TreeGridModel>();
            if (keyword != "" && keyword != null)
            {
                data = data.TreeWhere(c => c.F_FullName.Contains(keyword));
            }
            foreach (Sys_Items item in data)
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
            var data = itemsApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(Sys_Items itemsEntity, string keyValue)
        {
            try
            {
                bool i = false;
                Sys_User uInfo = ViewData["Account"] as Sys_User;
                if (keyValue == "" || keyValue == null)
                {
                    itemsEntity.F_CreatorUserId = uInfo.F_Account;
                    itemsEntity.F_CreatorTime = DateTime.Now;
                    itemsEntity.F_Id = System.Guid.NewGuid().ToString();
                    string[] notstr = { "ChildNodes" };
                    i = itemsApp.Add(itemsEntity, notstr);
                }
                else
                {
                    itemsEntity.F_Id = keyValue;
                    itemsEntity.F_LastModifyUserId = uInfo.F_Account;
                    itemsEntity.F_LastModifyTime = DateTime.Now;
                    string[] notstr = { "ChildNodes", "F_CreatorUserId", "F_CreatorTime", "F_ParentId" };
                    i = itemsApp.Update(itemsEntity, notstr);
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
                i = itemsApp.Delete(keyValue);
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
