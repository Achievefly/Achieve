using AchieveEntity;
using System.Collections.Generic;
using System.Data;
using AchieveCommon;
using AchieveCommon.Web;

namespace AchieveInterfaceDAL
{
    /// <summary>
    /// 用户接口（不同的数据库访问类实现接口达到多数据库的支持）
    /// </summary>
    public interface ISys_UserDAL
    {

        /// <summary>
        /// 用户登录
        /// </summary>
        Sys_User UserLogin(string loginId, string loginPwd);
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Sys_User GetUserById(string userId);

         /// <summary>
        /// 启用/禁用账户
        /// </summary>
        /// <param name="ID">用户id</param>
        /// <param name="IsAble">是否启用</param>
        /// <returns></returns>
        bool Startusing(string ID, int IsAble);
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<Sys_User> GetList(Pagination pagination, string keyword, out int records);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        int RevisePassword(string id,string password);

        int Add(Sys_User obj,string [] disstr);

        int Delete(string idstr);

        int Update(Sys_User obj, string[] disablestr = null);


    }
}
