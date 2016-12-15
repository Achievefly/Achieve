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
    public class OrganizeController : BaseController
    {
        private Sys_OrganizeBLL organizeApp = new Sys_OrganizeBLL();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = organizeApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (Sys_Organize item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.F_FullName;
                treeModel.parentId = item.F_ParentId;
                treeModel.data = item;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeJson()
        {
            var data = organizeApp.GetList();
            var treeList = new List<TreeViewModel>();
            foreach (Sys_Organize item in data)
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
        public ActionResult GetTreeGridJson(string keyword)
        {
            var data = organizeApp.GetList();
            if (!string.IsNullOrEmpty(keyword))
            {
                //data = Seach(data, keyword);
                data = data.TreeWhere(t => t.F_FullName.Contains(keyword));
            }
            var treeList = new List<TreeGridModel>();
            foreach (Sys_Organize item in data)
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
            var data = organizeApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(Sys_Organize organizeEntity, string keyValue)
        {
            try
            {
                bool i = false;
                if (keyValue == "" || keyValue == null)
                {
                    organizeEntity.F_CreatorUserId = OperatorProvider.Provider.GetCurrent().UserCode;
                    organizeEntity.F_CreatorTime = DateTime.Now;
                    organizeEntity.F_Id = System.Guid.NewGuid().ToString();
                    string[] notstr = { "ChildNodes" };
                    i = organizeApp.Add(organizeEntity, notstr);
                }
                else
                {
                    organizeEntity.F_Id = keyValue;
                    organizeEntity.F_LastModifyUserId = OperatorProvider.Provider.GetCurrent().UserCode;
                    organizeEntity.F_LastModifyTime = DateTime.Now;
                    string[] notstr = { "ChildNodes", "F_CreatorUserId", "F_CreatorTime" };
                    i = organizeApp.Update(organizeEntity, notstr);
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
                i = organizeApp.Delete(keyValue);
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
        [NonAction]
        public List<Sys_Organize> Seach(List<Sys_Organize> list, string keyValue)
        {
            List<Sys_Organize> treeList = new List<Sys_Organize>();
            foreach (Sys_Organize entity in list.Where(c => c.F_FullName.Contains(keyValue)))
            {
                treeList.Add(entity);
                string pId = entity.F_ParentId;
                while (true)
                {
                    if (string.IsNullOrEmpty(pId) && pId == "0")
                    {
                        break;
                    }
                    var upRecord = list.Where(c => c.F_Id == pId).ToList();
                    if (upRecord != null && upRecord.Count > 0)
                    {
                        treeList.Add(upRecord[0]);
                        pId = upRecord[0].F_ParentId;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return treeList;
        }
    }
}
