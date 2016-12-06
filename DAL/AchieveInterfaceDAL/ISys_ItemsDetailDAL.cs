using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchieveEntity;

namespace AchieveInterfaceDAL
{
    public interface ISys_ItemsDetailDAL
    {

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<Sys_ItemsDetail> GetList(string id = "",string keyvalue="");
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        Sys_ItemsDetail GetForm(string id);
        /// <summary>
        /// 根据编码获取数据
        /// </summary>
        /// <param name="encode"></param>
        /// <returns></returns>
        List<Sys_ItemsDetail> GetItemList(string encode);

        int Add(Sys_ItemsDetail obj, string[] disstr);

        int Delete(string idstr);

        int Update(Sys_ItemsDetail obj, string[] disablestr = null);
    }
}
