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
    public class Sys_ModuleButtonDAL : BaseDAL<Sys_ModuleButton>, ISys_ModuleButtonDAL
    {
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        public List<Sys_ModuleButton> GetAllModule()
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                List<Sys_ModuleButton> list = db.Queryable<Sys_ModuleButton>().Where(c => c.F_ParentId == "0").ToList();
                if (list.Count > 0)
                {
                    //foreach (var c in list)
                    //{
                    //    List<Sys_Module> listch = GetList(c.F_Id);
                    //    if (listch != null)
                    //    {
                    //        c.ChildNodes = listch;
                    //    }
                    //}
                    return list;
                }
                else
                {
                    return null;
                }
            }

        }
        public List<Sys_ModuleButton> GetList(string id = "")
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                List<Sys_ModuleButton> list = null;
                var data = db.Queryable<Sys_ModuleButton>();
                list = data.ToList();
                if (id != "" && id != null)
                {
                    list = data.Where(c => c.F_ModuleId == id).ToList();
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
        /// id获取菜单按钮
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sys_ModuleButton GetForm(string id)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                var data = db.Queryable<Sys_ModuleButton>().Where(c => c.F_Id == id).ToList();
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
        /// 根据角色获取按钮
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public List<Sys_ModuleButton> GetButtonList(string roleid)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                var data = db.SqlQuery<Sys_ModuleButton>("select * from Sys_ModuleButton where F_Id in (select F_ItemId from [Sys_RoleAuthorize]   WHERE [F_ObjectId]  ='" + roleid + "')").ToList();
                return data;
            }
        }
    }
}
