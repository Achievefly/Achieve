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
    public class Sys_ItemsDAL : BaseDAL<Sys_Items>, ISys_ItemsDAL
    {
        /// <summary>
        /// 获取所有机构
        /// </summary>
        public List<Sys_Items> GetAllModule()
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                List<Sys_Items> list = db.Queryable<Sys_Items>().Where(c => c.F_ParentId == "0").ToList();
                if (list.Count > 0)
                {
                    foreach (var c in list)
                    {
                        List<Sys_Items> listch = GetList(c.F_Id);
                        if (listch != null)
                        {
                            c.ChildNodes = listch;
                        }
                    }
                    return list;
                }
                else
                {
                    return null;
                }
            }

        }
        public List<Sys_Items> GetList(string id = "")
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                List<Sys_Items> list = null;
                var data = db.Queryable<Sys_Items>();
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
        /// id获取机构
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sys_Items GetForm(string id)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                var data = db.Queryable<Sys_Items>().Where(c => c.F_Id == id).ToList();
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
