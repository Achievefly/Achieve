using AchieveCommon;
using AchieveDALFactory;
using AchieveEntity;
using AchieveInterfaceDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchieveBLL
{
    public class Sys_RoleAuthorizeBLL
    {
        ISys_RoleAuthorizeDAL dal = DALFactory.GetRoleAuthorizeDAL();

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        public List<Sys_RoleAuthorize> GetList(string id = "")
        {
            return dal.GetList(id);
        }

        public Sys_RoleAuthorize GetForm(string id)
        {
            return dal.GetForm(id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public bool Add(Sys_RoleAuthorize obj, string[] disstr = null)
        {
            return dal.Add(obj, disstr);
        }
        public bool Delete(string idstr)
        {
            return dal.Delete(idstr);
        }
        public bool Update(Sys_RoleAuthorize obj, string[] disablestr = null)
        {
            return dal.Update(obj, disablestr);
        }
    }
}
