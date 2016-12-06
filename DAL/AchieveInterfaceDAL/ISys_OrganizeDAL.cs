using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchieveEntity;

namespace AchieveInterfaceDAL
{
    public interface ISys_OrganizeDAL
    {
        /// <summary>
        /// 获取所有机构
        /// </summary>
        /// <returns></returns>
        List<Sys_Organize> GetAllModule();

        /// <summary>
        /// 获取所有机构
        /// </summary>
        /// <returns></returns>
        List<Sys_Organize> GetList(string id = "");
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        Sys_Organize GetForm(string id);

        int Add(Sys_Organize obj, string[] disstr);

        int Delete(string idstr);

        int Update(Sys_Organize obj, string[] disablestr = null);
    }
}
