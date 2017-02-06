using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AchieveManageWeb.BLL;

namespace AchieveManageWeb.apis
{
    /// <summary>
    /// gatadata 的摘要说明
    /// </summary>
    public class getdata : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //这里的类型要改成json，否则，前端获取的数据需要调用JSON.parse方法将文本转成json，
            //为了不用改变前端代码，这里将text/plain改为application/json
            context.Response.ContentType = "application/json";
            //接收type 参数
            string type = context.Request.QueryString["type"] ?? context.Request.QueryString["type"].ToString();
            //调用业务处理方法获取数据结果
            var result = DBHelper.GetResult(type);
            //序列化
            var json = MessageUtils.ScriptSerialize(result);
            context.Response.Write(json);
        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}