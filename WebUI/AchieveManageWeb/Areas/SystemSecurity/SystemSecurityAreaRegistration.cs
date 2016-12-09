using System.Web.Mvc;

namespace AchieveManageWeb.Areas.SystemSecurity
{
    public class SystemSecurityAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SystemSecurity";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                 this.AreaName + "_default",
                 this.AreaName + "/{controller}/{action}/{id}",
                new { area = this.AreaName, action = "Index", id = UrlParameter.Optional },
                new string[] { "AchieveManageWeb.Areas." + this.AreaName + ".Controllers" }
            );
        }
    }
}
