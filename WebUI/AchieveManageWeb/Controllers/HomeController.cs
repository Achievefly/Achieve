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
        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <param name="id">所属</param>
        /// <returns>树</returns>
        public ActionResult GetAllTreeByH(string id)
        {
            try
            {
                Sys_User uInfo = ViewData["Account"] as Sys_User;
                if (uInfo != null)
                {
                //    DataTable dt = new MenuBLL().GetUserMenuData(uInfo.ID, id);
                //    List<SysModuleNavModel> list = new List<SysModuleNavModel>();
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        SysModuleNavModel model = new SysModuleNavModel();
                //        model.id = dt.Rows[i]["menuid"].ToString();
                //        model.text = dt.Rows[i]["menuname"].ToString();
                //        model.attributes = dt.Rows[i]["linkaddress"].ToString();
                //        model.iconCls = dt.Rows[i]["icon"].ToString();
                //        model.sort = dt.Rows[i]["menusort"].ToString();
                //        List<SysModuleNavModel> listtwo = new List<SysModuleNavModel>();
                //        DataTable dttwo = new MenuBLL().GetUserMenuData(uInfo.ID, dt.Rows[i]["menuid"].ToString());
                //        for (int j = 0; j < dttwo.Rows.Count; j++)
                //        {
                //            SysModuleNavModel models = new SysModuleNavModel();
                //            models.id = dttwo.Rows[j]["menuid"].ToString();
                //            models.text = dttwo.Rows[j]["menuname"].ToString();
                //            models.attributes = dttwo.Rows[j]["linkaddress"].ToString();
                //            models.iconCls = dttwo.Rows[j]["icon"].ToString();
                //            models.sort = dttwo.Rows[j]["menusort"].ToString();
                //            listtwo.Add(models);
                //        }
                //        model.children = listtwo;
                //        list.Add(model);
                //    }
                    return Json(new Sys_ModuleBLL().GetAllModule());
                }
                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
