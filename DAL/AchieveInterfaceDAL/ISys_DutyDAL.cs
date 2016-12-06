using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchieveEntity;

namespace AchieveInterfaceDAL
{
    public interface ISys_DutyDAL
    {
        /// <summary>
        /// 获取所有岗位
        /// </summary>
        /// <returns></returns>
        List<Sys_Role> GetList();
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        Sys_Role GetForm(string id);

        int Add(Sys_Role obj, string[] disstr);

        int Delete(string idstr);

        int Update(Sys_Role obj, string[] disablestr = null);
    }
}
