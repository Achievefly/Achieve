using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AchieveManageWeb.App_Start.BaseController;
using AchieveManageWeb.BLL;

namespace AchieveManageWeb.Areas.ExampleManage.Controllers
{
    public class LayMiController : BaseController
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public JsonResult GetData(string type)
        {
            var result = DBHelper.GetResult(type);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
