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
    public class Sys_ModuleDAL : BaseDAL<Sys_Module>,ISys_ModuleDAL
    {
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        public List<Sys_Module> GetAllModule()
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                List<Sys_Module> list = db.Queryable<Sys_Module>().Where(c=>c.F_ParentId=="0").ToList();
                if (list.Count > 0)
                {
                    foreach (var c in list)
                    {
                        List<Sys_Module> listch = GetList(c.F_Id);
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
        public override List<Sys_Module> GetList(string id="")
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                List<Sys_Module> list = null;
                var data = db.Queryable<Sys_Module>();
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
        /// id获取菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sys_Module GetForm(string id)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                var data = db.Queryable<Sys_Module>().Where(c => c.F_Id == id).ToList();
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
        /// 角色获取菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Sys_Module> GetMRoleList(string id)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                var data = db.SqlQuery<Sys_Module>("select * from Sys_Module where F_Id in (select F_ItemId from [Sys_RoleAuthorize]   WHERE [F_ObjectId]  ='" + id + "')");
                List<Sys_Module> list = data.Where(c => c.F_ParentId == "0").ToList();
                if (list.Count > 0)
                {
                    foreach (var c in list)
                    {
                        List<Sys_Module> listch = data.Where(a=>a.F_ParentId==c.F_Id).ToList();
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
    }
}
