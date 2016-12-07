using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchieveEntity;

namespace AchieveInterfaceDAL
{
    public interface ISys_RoleAuthorizeDAL
    {

        /// <summary>
        /// 获取所有机构
        /// </summary>
        /// <returns></returns>
        List<Sys_RoleAuthorize> GetList(string id = "");
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        Sys_RoleAuthorize GetForm(string table, string id);

        int Add(Sys_RoleAuthorize obj, string[] disstr);

        int Delete(string idstr);

        int Update(Sys_RoleAuthorize obj, string[] disablestr = null);
    }
}
