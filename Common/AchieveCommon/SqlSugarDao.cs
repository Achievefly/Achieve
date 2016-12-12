using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchieveCommon.Base;
using SqlSugar;

namespace AchieveCommon
{
    public class SqlSugarDao
    {
        private SqlSugarDao()
        {

        }
        //读取配置文件里的数据库连接字符串
        public static readonly string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public static string ConnectionString
        {
            get
            {
                string reval = connStr;
                return reval;
            }
        }
        public static SqlSugarClient GetInstance()
        {
            var db = new SqlSugarClient(ConnectionString);
            db.IsEnableAttributeMapping = true;//启用属性映射
            db.IsEnableLogEvent = true;//启用监控
            db.IsIgnoreErrorColumns = true;//忽略非数据库列
            db.LogEventStarting = (sql, par) => { AchieveCommon.WriteLog.WriteMessage("SQLLogInfo", sql + " " + par + "\r\n"); };
            return db;
        }
    }
}
