using AchieveCommon;
using AchieveDALFactory;
using AchieveEntity;
using AchieveInterfaceDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchieveCommon.Web;

namespace AchieveBLL
{
    public class Sys_LogBLL
    {
        ISys_LogDAL dal = DALFactory.GetLogDAL();

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public List<Sys_Log> GetPageList(Pagination pagination, string keyword, out int records)
        {
            return dal.GetPageList(pagination, keyword, out records);
        }

        public Sys_Log GetForm(string id)
        {
            return dal.GetForm(id);
        }

        public bool Remove(string keeptime)
        {
            return dal.Remove(keeptime);
        }
    }
}
