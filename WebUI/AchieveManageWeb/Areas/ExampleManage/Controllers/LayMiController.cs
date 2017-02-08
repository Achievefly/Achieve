using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AchieveBLL;
using AchieveCommon.Operator;
using AchieveEntity;
using AchieveManageWeb.App_Start.BaseController;

namespace AchieveManageWeb.Areas.ExampleManage.Controllers
{
    public class LayMiController : BaseController
    {
        private Sys_LayimBLL layApp = new Sys_LayimBLL();
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public JsonResult GetData(string type,string groupid)
        {
            CSResult result = null;
            switch (type)
            {
                case "friend":
                    result = layApp.GetAllfriend(OperatorProvider.Provider.GetCurrent().UserId);
                    break;
                case "group":
                    result = layApp.GetAllGroup(OperatorProvider.Provider.GetCurrent().UserId);
                    break;
                case "log":
                    result = null;
                    break;
                case "groups":
                    result = layApp.GetAllGroup(groupid);
                    break;
                default:
                    result = new CSResult { status = 0, data = null, msg = "无效的请求类型" };
                    break;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
