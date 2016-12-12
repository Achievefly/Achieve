using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AchieveManageWeb.App_Start.BaseController;

namespace AchieveManageWeb.Areas.ReportManage.Controllers
{
    public class EchartsController : BaseController
    {
        //
        // GET: /ReportManage/Echarts/

        public ActionResult Index()
        {
            return View();
        }

    }
}
