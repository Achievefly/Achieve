using AchieveBLL;
using AchieveCommon;
using AchieveEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AchieveManageWeb.App_Start.BaseController;

namespace AchieveManageWeb.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            Sys_User uInfo = ViewData["Account"] as Sys_User;
            if (uInfo == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.RealName = uInfo.F_RealName;
            ViewBag.TimeView = DateTime.Now.ToLongDateString();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
