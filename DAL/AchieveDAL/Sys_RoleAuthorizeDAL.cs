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
    public class Sys_RoleAuthorizeDAL : BaseDAL<Sys_RoleAuthorize>, ISys_RoleAuthorizeDAL
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override List<Sys_RoleAuthorize> GetList(string id = "")
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                List<Sys_RoleAuthorize> list = null;
                var data = db.Queryable<Sys_RoleAuthorize>();
                if (id != "" && id != null)
                {
                    data = data.Where(c => c.F_ObjectId == id);
                }
                list = data.ToList();
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
    }
}
