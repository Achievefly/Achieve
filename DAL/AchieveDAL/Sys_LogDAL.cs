using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchieveCommon;
using AchieveCommon.Base;
using AchieveCommon.Web;
using AchieveEntity;
using AchieveInterfaceDAL;
using SqlSugar;

namespace AchieveDAL
{
    public class Sys_LogDAL : ISys_LogDAL
    {

        public List<Sys_Log> GetPageList(Pagination pagination, string queryJson, out int records)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                var pageCount = 0;
                List<Sys_Log> list = null;
                var queryParam = queryJson.ToJObject();
                var data = db.Queryable<Sys_Log>();
                if (queryParam["keyword"]!=null&&queryParam["keyword"].ToString()!="")
                {
                    string keyword = queryParam["keyword"].ToString();
                    data = data.
                        Where(c => c.F_Account.Contains(keyword) || c.F_NickName.Contains(keyword) || c.F_Description.Contains(keyword));
                    //OrderBy(pagination.sidx + " " + pagination.sord).ToPageList(pagination.page, pagination.rows, ref pageCount);
                }

                if (queryParam["timeType"] == null || queryParam["timeType"].ToString() == "")
                {
                    queryParam["timeType"] = "2";
                }
                    string timeType = queryParam["timeType"].ToString();
                    DateTime startTime = DateTime.Now.ToString("yyyy-MM-dd").ObjToDate();
                    DateTime endTime = DateTime.Now.ToString("yyyy-MM-dd").ObjToDate().AddDays(1);
                    switch (timeType)
                    {
                        case "1":
                            break;
                        case "2":
                            startTime = DateTime.Now.AddDays(-7);
                            break;
                        case "3":
                            startTime = DateTime.Now.AddMonths(-1);
                            break;
                        case "4":
                            startTime = DateTime.Now.AddMonths(-3);
                            break;
                        default:
                            break;
                    }
                    data = data.Where(t => t.F_Date >= startTime && t.F_Date <= endTime);
                if (data != null)
                {
                    list = data.OrderBy(pagination.sidx + " " + pagination.sord).ToPageList(pagination.page, pagination.rows, ref pageCount);
                    records = pageCount;
                }
                else
                {
                    list = null;
                    records = 0;
                }
                
                return list;
            }
        }
        /// <summary>
        /// id获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sys_Log GetForm(string id)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                var data = db.Queryable<Sys_Log>().Where(c => c.F_Id == id).ToList();
                if (data.Count > 0)
                {
                    return data[0];
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 清空数据
        /// </summary>
        /// <returns></returns>
        public bool Remove(string keeptime)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                DateTime operateTime = DateTime.Now;
                if (keeptime == "7")            //保留近一周
                {
                    operateTime = DateTime.Now.AddDays(-7);
                }
                else if (keeptime == "1")       //保留近一个月
                {
                    operateTime = DateTime.Now.AddMonths(-1);
                }
                else if (keeptime == "3")       //保留近三个月
                {
                    operateTime = DateTime.Now.AddMonths(-3);
                }
                bool i = db.Delete<Sys_Log>(c => c.F_Date <= operateTime).ObjToBool();
                return true;
            }
        }
    }
}
