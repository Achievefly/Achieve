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
    public class Sys_FileDAL : BaseDAL<Sys_File>, ISys_FileDAL
    {
        public List<Sys_File> GetList(string id = "")
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                List<Sys_File> list = null;
                var data = db.Queryable<Sys_File>();
                list = data.ToList();
                if (id != "" && id != null)
                {
                    list = data.Where(c => c.F_ParentId == id).ToList();
                }
                if (list.Count > 0)
                {
                    return list;
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="records"></param>
        /// <returns></returns>
        public List<Sys_File> GetPageList(Pagination pagination, string keyword, out int records)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                var pageCount = 0;
                List<Sys_File> list = null;
                var data = db.Queryable<Sys_File>();
                if (keyword != null && keyword != "")
                {
                    data = data.Where(c => c.F_FileType == keyword);
                }
                list = data.OrderBy(pagination.sidx + " " + pagination.sord).ToPageList(pagination.page, pagination.rows, ref pageCount);
                records = pageCount;
                return list;
            }
        }
    }
}
