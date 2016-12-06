using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchieveEntity;

namespace AchieveInterfaceDAL
{
    public interface ISys_AreaDAL
    {
        /// <summary>
        /// 获取所有机构
        /// </summary>
        /// <returns></returns>
        List<Sys_Area> GetAllModule();

        /// <summary>
        /// 获取所有机构
        /// </summary>
        /// <returns></returns>
        List<Sys_Area> GetList(string id = "");
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        Sys_Area GetForm(string id);

        int Add(Sys_Area obj, string[] disstr);

        int Delete(string idstr);

        int Update(Sys_Area obj, string[] disablestr = null);
    }
}
