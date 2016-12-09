using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchieveEntity;
using AchieveCommon.Web;

namespace AchieveInterfaceDAL
{
    public interface ISys_LogDAL
    {
        /// <summary>
        /// 获取所有机构
        /// </summary>
        /// <returns></returns>
        List<Sys_Log> GetPageList(Pagination pagination, string keyword, out int records);
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        Sys_Log GetForm(string id);

        bool Remove(string keeptiem);
    }
}
