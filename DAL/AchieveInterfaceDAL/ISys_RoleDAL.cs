using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchieveEntity;

namespace AchieveInterfaceDAL
{
    public interface ISys_RoleDAL
    {

        /// <summary>
        /// 获取所有机构
        /// </summary>
        /// <returns></returns>
        List<Sys_Role> GetList(string id = "");
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        Sys_Role GetForm(string table, string id);

        int Add(Sys_Role obj, List<Sys_RoleAuthorize> disstr,bool isadd);

        int Delete(string idstr);
    }
}
