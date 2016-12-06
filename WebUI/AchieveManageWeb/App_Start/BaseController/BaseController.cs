using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AchieveCommon;
using AchieveCommon.Web;

namespace AchieveManageWeb.App_Start.BaseController
{
    [AchieveManageWeb.App_Start.JudgmentLogin]
    public abstract class BaseController : Controller
    {
        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public virtual ActionResult Add()
        {
            return View();
        }
        [HttpGet]
        public virtual ActionResult Edit()
        {
            return View();
        }
        [HttpGet]
        public virtual ActionResult Details()
        {
            return View();
        }
        [HttpGet]
        public virtual ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 成功消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message)
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = message });
        }
        /// <summary>
        /// 成功消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message, object data)
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data });
        }
        /// <summary>
        /// 错误消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual ActionResult Error(string message)
        {
            return Json(new AjaxResult { state = ResultType.error.ToString(), message = message });
        }
        /// <summary>
        /// 普通消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual ActionResult Info(string message)
        {
            return Json(new AjaxResult { state = ResultType.info.ToString(), message = message });
        }
        /// <summary>
        /// 警告消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual ActionResult Warning(string message)
        {
            return Json(new AjaxResult { state = ResultType.warning.ToString(), message = message });
        }
    }
}