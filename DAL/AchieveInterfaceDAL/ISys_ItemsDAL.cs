using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchieveEntity;

namespace AchieveInterfaceDAL
{
    public interface ISys_ItemsDAL
    {
        /// <summary>
        /// 获取所有机构
        /// </summary>
        /// <returns></returns>
        List<Sys_Items> GetAllModule();

        /// <summary>
        /// 获取所有机构
        /// </summary>
        /// <returns></returns>
        List<Sys_Items> GetList(string id = "");
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        Sys_Items GetForm(string id);

        int Add(Sys_Items obj, string[] disstr);

        int Delete(string idstr);

        int Update(Sys_Items obj, string[] disablestr = null);
    }
}
