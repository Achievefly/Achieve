using AchieveCommon;
using AchieveDALFactory;
using AchieveEntity;
using AchieveInterfaceDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchieveCommon.Web;

namespace AchieveBLL
{
    public class Sys_FileBLL
    {
        ISys_FileDAL dal = DALFactory.GetFileDAL();

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<Sys_File> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<Sys_File> GetPageList(Pagination pagination, string keyword, out int records)
        {
            return dal.GetPageList(pagination, keyword, out records);
        }

        public Sys_File GetForm(string id)
        {
            return dal.GetForm(id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public bool Add(Sys_File obj, string[] disstr = null)
        {
            return dal.Add(obj, disstr);
        }
        public bool Delete(string idstr)
        {
            return dal.Delete(idstr);
        }
        public bool Update(Sys_File obj, string[] disablestr = null)
        {
            return dal.Update(obj, disablestr);
        }
    }
}
