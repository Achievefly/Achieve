using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AchieveManageWeb.Startup))]

namespace AchieveManageWeb
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/cs", map =>
            {
                var hubConfiguration = new HubConfiguration()
                {
                    EnableJSONP = true
                };
                map.RunSignalR(hubConfiguration);
            });
        }
    }
}
