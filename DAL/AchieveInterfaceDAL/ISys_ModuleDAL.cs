using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchieveEntity;

namespace AchieveInterfaceDAL
{
    public interface ISys_ModuleDAL
    {
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        List<Sys_Module> GetAllModule();

        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        List<Sys_Module> GetList(string id="");
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        Sys_Module GetForm(string id);

        int Add(Sys_Module obj, string[] disstr);

        int Delete(string idstr);

        int Update(Sys_Module obj, string[] disablestr = null);
    }
}
