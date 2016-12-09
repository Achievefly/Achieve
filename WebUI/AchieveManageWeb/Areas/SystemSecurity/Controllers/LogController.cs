using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AchieveBLL;
using AchieveCommon;
using AchieveCommon.Web;
using AchieveManageWeb.App_Start;
using AchieveManageWeb.App_Start.BaseController;

namespace AchieveManageWeb.Areas.SystemSecurity.Controllers
{
    public class LogController : BaseController
    {
        private Sys_LogBLL logApp = new Sys_LogBLL();

        [HttpGet]
        public ActionResult RemoveLog()
        {
            return View();
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string queryJson)
        {
            int records = 0;
            var data = new
            {
                rows = logApp.GetPageList(pagination, queryJson, out records),
                total = records % pagination.rows == 0 ? records / pagination.rows : records / pagination.rows + 1,
                page = pagination.page,
                records = records
            };
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitRemoveLog(string keepTime)
        {
            try
            {
                bool i = false;
                i = logApp.Remove(keepTime);
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
    }
}
