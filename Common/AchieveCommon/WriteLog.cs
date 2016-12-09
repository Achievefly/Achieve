using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AchieveEntity;

namespace AchieveCommon
{
    public class WriteLog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="ex"></param>
        public static void WriteErorrLog(string fileName, Exception ex)
        {
            if (ex == null) return; //ex = null 返回  
            DateTime dt = DateTime.Now; // 设置日志时间  
            string time = dt.ToString("yyyy-MM-dd HH:mm:ss"); //年-月-日 时：分：秒  
            string logName = dt.ToString("yyyy-MM-dd"); //日志名称  
            string logPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, Path.Combine("log", fileName)); //日志存放路径  
            string log = Path.Combine(logPath, string.Format("{0}.log", logName)); //路径 + 名称
            try
            {
                FileInfo info = new FileInfo(log);
                if (info.Directory != null && !info.Directory.Exists)
                {
                    info.Directory.Create();
                }
                using (StreamWriter write = new StreamWriter(log, true, Encoding.GetEncoding("utf-8")))
                {
                    write.WriteLine(time);
                    write.WriteLine(ex.Message);
                    write.WriteLine("异常信息：" + ex);
                    write.WriteLine("异常堆栈：" + ex.StackTrace);
                    write.WriteLine("异常简述：" + ex.Message);
                    write.WriteLine("\r\n----------------------------------\r\n");
                    write.Flush();
                    write.Close();
                    write.Dispose();
                }
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="message"></param>
        public static void WriteMessage(string fileName, string message)
        {
            //ex = null 返回  
            DateTime dt = DateTime.Now; // 设置日志时间  
            string time = dt.ToString("yyyy-MM-dd HH:mm:ss"); //年-月-日 时：分：秒  
            string logName = dt.ToString("yyyy-MM-dd"); //日志名称  
            string logPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, Path.Combine("log", fileName)); //日志存放路径  
            string log = Path.Combine(logPath, string.Format("{0}.log", logName)); //路径 + 名称
            try
            {
                FileInfo info = new FileInfo(log);
                if (info.Directory != null && !info.Directory.Exists)
                {
                    info.Directory.Create();
                }
                using (StreamWriter write = new StreamWriter(log, true, Encoding.GetEncoding("utf-8")))
                {
                    write.WriteLine(time);
                    write.WriteLine("信息：" + message);
                    write.WriteLine("\r\n----------------------------------\r\n");
                    write.Flush();
                    write.Close();
                    write.Dispose();
                }
            }
            catch { }
        }


        public static void WriteErorrLog(Exception ex, string message)
        {
            if (ex == null) return; //ex = null 返回  
            DateTime dt = DateTime.Now; // 设置日志时间  
            string time = dt.ToString("yyyy-MM-dd HH:mm:ss"); //年-月-日 时：分：秒  
            string logName = dt.ToString("yyyy-MM-dd"); //日志名称  
            string logPath = System.AppDomain.CurrentDomain.BaseDirectory; //日志存放路径  
            string log = Path.Combine(Path.Combine(logPath, "log"), string.Format("{0}.log", logName)); //路径 + 名称
            try
            {
                FileInfo info = new FileInfo(log);
                if (info.Directory != null && !info.Directory.Exists)
                {
                    info.Directory.Create();
                }
                using (StreamWriter write = new StreamWriter(log, true, Encoding.GetEncoding("utf-8")))
                {
                    write.WriteLine(time);
                    write.WriteLine(ex.Message);
                    write.WriteLine("异常信息：" + ex);
                    write.WriteLine("异常堆栈：" + ex.StackTrace);
                    write.WriteLine("异常简述：" + message);
                    write.WriteLine("\r\n----------------------------------\r\n");
                    write.Flush();
                    write.Close();
                    write.Dispose();
                }
            }
            catch { }
        }

        public static void WriteMessage(string message)
        {
            //ex = null 返回  
            DateTime dt = DateTime.Now; // 设置日志时间  
            string time = dt.ToString("yyyy-MM-dd HH:mm:ss"); //年-月-日 时：分：秒  
            string logName = dt.ToString("yyyy-MM-dd"); //日志名称  
            string logPath = System.AppDomain.CurrentDomain.BaseDirectory; //日志存放路径  
            string log = Path.Combine(Path.Combine(logPath, "log"), string.Format("{0}.log", logName)); //路径 + 名称
            try
            {
                FileInfo info = new FileInfo(log);
                if (info.Directory != null && !info.Directory.Exists)
                {
                    info.Directory.Create();
                }
                using (StreamWriter write = new StreamWriter(log, true, Encoding.GetEncoding("utf-8")))
                {
                    write.WriteLine(time);
                    write.WriteLine("信息：" + message);
                    write.WriteLine("\r\n----------------------------------\r\n");
                    write.Flush();
                    write.Close();
                    write.Dispose();
                }
            }
            catch { }
        }

        public static void WriteDatalog(string jsonstrstart, string jsongstrend, bool result, string description,string typestr)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                Sys_Log log = new Sys_Log();
                log.F_Id = System.Guid.NewGuid().ToString();
                log.F_Account = CookiesHelper.GetCookieValue("UserAccount");
                log.F_CreatorTime = DateTime.Now;
                log.F_CreatorUserId = "system";
                log.F_Date = DateTime.Now;
                log.F_NickName = CookiesHelper.GetCookieValue("UserName");
                log.F_IPAddress = Net.Net.Ip;
                log.F_IPAddressName = Net.Net.GetLocation(log.F_IPAddress);
                log.F_Result = result;
                log.F_Description = description;
                log.F_Before = jsonstrstart;
                log.F_Later = jsongstrend;
                log.F_Type = typestr;
                db.Insert(log);

            }
        }
    }
}
