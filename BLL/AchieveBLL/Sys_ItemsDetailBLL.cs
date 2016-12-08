using AchieveCommon;
using AchieveDALFactory;
using AchieveEntity;
using AchieveInterfaceDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchieveBLL
{
    public class Sys_ItemsDetailBLL
    {
        ISys_ItemsDetailDAL dal = DALFactory.GetItemsDetailDAL();

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<Sys_ItemsDetail> GetList(string itemsid="",string keyvale="")
        {
            return dal.GetList(itemsid,keyvale);
        }

        public Sys_ItemsDetail GetForm(string id)
        {
            return dal.GetForm(id);
        }
        /// <summary>
        /// 根据编码查询items
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Sys_ItemsDetail> GetItemList(string encode)
        {
            return dal.GetItemList(encode);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public bool Add(Sys_ItemsDetail obj, string[] disstr = null)
        {
            return dal.Add(obj, disstr);
        }
        public bool Delete(string idstr)
        {
            return dal.Delete(idstr);
        }
        public bool Update(Sys_ItemsDetail obj, string[] disablestr = null)
        {
            return dal.Update(obj, disablestr);
        }
    }
}
