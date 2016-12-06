using AchieveCommon;
using AchieveDALFactory;
using AchieveEntity;
using AchieveInterfaceDAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AchieveCommon.Web;

namespace AchieveBLL
{
    /// <summary>
    /// 用户（BLL）
    /// </summary>
    public class Sys_UserBLL
    {
        ISys_UserDAL dal = DALFactory.GetUserDAL();

        /// <summary>
        /// 用户登录
        /// </summary>
        public Sys_User UserLogin(string loginId, string loginPwd)
        {
            return dal.UserLogin(loginId, loginPwd);
        }
        /// <summary>
        /// id获取用户信息
        /// </summary>
        public Sys_User GetUserById(string userId)
        {
            return dal.GetUserById(userId);
        }
         /// <summary>
        /// 启用/禁用账户
        /// </summary>
        /// <param name="ID">用户id</param>
        /// <param name="IsAble">是否启用</param>
        /// <returns></returns>
        public bool Startusing(string ID, int IsAble)
        {
            return dal.Startusing(ID,IsAble);
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="records"></param>
        /// <returns></returns>
        public List<Sys_User> GetList(Pagination pagination, string keyword, out int records)
        {
            return dal.GetList(pagination, keyword, out records);
        }

        public int RevisePassword(string id, string password)
        {
            return dal.RevisePassword(id,password);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public int Add(Sys_User obj,string[] disstr=null)
        {
            return dal.Add(obj,disstr);
        }
        public int Delete(string idstr)
        {
            return dal.Delete(idstr);
        }
        public int Update(Sys_User obj, string[] disablestr = null)
        {
            return dal.Update(obj,disablestr);
        }

    }
}
