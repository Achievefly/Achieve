using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchieveEntity;

namespace AchieveInterfaceDAL
{
    public interface ISys_ModuleButtonDAL
    {
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        List<Sys_ModuleButton> GetAllModule();

        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        List<Sys_ModuleButton> GetList(string id = "");
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        Sys_ModuleButton GetForm(string id);

        int Add(Sys_ModuleButton obj, string[] disstr);

        int Delete(string idstr);

        int Update(Sys_ModuleButton obj, string[] disablestr = null);
    }
}
