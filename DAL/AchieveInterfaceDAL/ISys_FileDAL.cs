using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchieveCommon.Web;
using AchieveEntity;

namespace AchieveInterfaceDAL
{
    public interface ISys_FileDAL
    {

        /// <summary>
        /// 获取所有机构
        /// </summary>
        /// <returns></returns>
        List<Sys_File> GetList(string id = "");

        List<Sys_File> GetPageList(Pagination pagination, string keyword, out int records);
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        Sys_File GetForm(string id);

        bool Add(Sys_File obj, string[] disstr);

        bool Delete(string idstr);

        bool Update(Sys_File obj, string[] disablestr = null);
    }
}
