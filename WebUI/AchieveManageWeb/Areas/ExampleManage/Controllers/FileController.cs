using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AchieveBLL;
using AchieveCommon.Operator;
using AchieveCommon;
using AchieveEntity;
using AchieveManageWeb.App_Start;
using AchieveManageWeb.App_Start.BaseController;
using AchieveCommon.Web;

namespace AchieveManageWeb.Areas.ExampleManage.Controllers
{
    public class FileController : BaseController
    {
        private Sys_FileBLL fileApp = new Sys_FileBLL();
        [HttpPost]
        public ActionResult SubmitForm(Sys_File fileEntity)
        {
            try
            {
                bool i = false;
                fileEntity.F_CreatorUserId = OperatorProvider.Provider.GetCurrent().UserCode;
                fileEntity.F_CreatorTime = DateTime.Now;
                fileEntity.F_Id = System.Guid.NewGuid().ToString();
                i = fileApp.Add(fileEntity);
                if (i)
                {
                    return Success("操作成功。");
                }
                else
                {
                    return Warning("操作失败。");
                }

            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult GetlistJson(int Page, int pagesize, string keyword)
        {
            Pagination pagination = new Pagination();
            pagination.page = Page == null ? 1 : Page;
            pagination.rows = pagesize;
            pagination.sidx = "F_Id";
            int records = 0;
            var data = new
            {
                rows = fileApp.GetPageList(pagination, keyword, out records),
                total = records % pagination.rows == 0 ? records / pagination.rows : records / pagination.rows + 1,
                page = pagination.page,
                records = records
            };
            return Content(data.ToJson());
        }

    }
}
