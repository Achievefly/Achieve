﻿using System.Web.Mvc;

namespace AchieveManageWeb.Areas.SystemManage
{
    public class SystemManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SystemManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                this.AreaName + "_default",
                this.AreaName + "/{controller}/{action}/{id}",
                new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "AchieveManageWeb.Areas." + this.AreaName + ".Controllers" }
            );
        }
    }
}
