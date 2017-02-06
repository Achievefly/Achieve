using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AchieveManageWeb.BLL;

namespace AchieveManageWeb.apis
{
    /// <summary>
    /// getdata1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class getdataWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetResult(string type)
        {
            //调用业务处理方法获取数据结果
            var result = DBHelper.GetResult(type);
            var json = MessageUtils.ScriptSerialize(result);
            return json;
        }
    }
}
