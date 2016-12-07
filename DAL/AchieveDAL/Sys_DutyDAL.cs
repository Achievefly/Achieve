using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchieveCommon;
using AchieveCommon.Base;
using AchieveEntity;
using AchieveInterfaceDAL;
using SqlSugar;

namespace AchieveDAL
{
    public class Sys_DutyDAL : BaseDAL<Sys_Role>, ISys_DutyDAL
    {
        /// <summary>
        /// 获取所有机构
        /// </summary>
        public List<Sys_Role> GetList()
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                List<Sys_Role> list = db.Queryable<Sys_Role>().Where(c => c.F_Category == 2).ToList();
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
        /// id获取机构
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sys_Role GetForm(string id)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                var data = db.Queryable<Sys_Role>().Where(c => c.F_Id == id).ToList();
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
    }
}
