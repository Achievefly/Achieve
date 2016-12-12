using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AchieveManageWeb.App_Start.BaseController;

namespace AchieveManageWeb.Areas.ExampleManage.Controllers
{
    public class SendMailController : BaseController
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SendMail(string account, string title, string content)
        {
            
            return Success("发送成功。");
        }
    }
}
