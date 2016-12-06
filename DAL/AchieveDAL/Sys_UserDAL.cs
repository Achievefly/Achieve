using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using AchieveInterfaceDAL;
using AchieveCommon;
using AchieveEntity;
using SqlSugar;
using AchieveCommon.Base;
using AchieveCommon.Web;

namespace AchieveDAL
{
    /// <summary>
    /// 用户（SQL Server数据库实现）
    /// </summary>
    public class Sys_UserDAL : BaseDAL<Sys_User>,ISys_UserDAL
    {

        /// <summary>
        /// 用户登录
        /// </summary>
        public Sys_User UserLogin(string loginId, string loginPwd)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                List<Sys_User> list = db.Queryable<Sys_User>().Where(c => c.F_Account == loginId && c.F_Password == loginPwd).ToList();
                if (list.Count > 0)
                {
                    return list[0];
                }
                else
                {
                    return null;
                }
            }
            
        }
        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        public Sys_User GetUserById(string userId)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                List<Sys_User> list = db.Queryable<Sys_User>().Where(c => c.F_Id == userId).ToList();
                if (list.Count > 0)
                {
                    return list[0];
                }
                else
                {
                    return null;
                }
            }

        }

        /// <summary>
        /// 启用/禁用账户
        /// </summary>
        /// <param name="ID">用户id</param>
        /// <param name="IsAble">是否启用</param>
        /// <returns></returns>
        public bool Startusing(string ID, int IsAble)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                bool i = db.Update<Sys_User>(new { F_EnabledMark = IsAble }, c => c.F_Id == ID).ObjToBool();
                return i;
            }
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="records"></param>
        /// <returns></returns>
        public List<Sys_User> GetList(Pagination pagination, string keyword,out int records)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                List<Sys_User> list = null;
                var data = db.Queryable<Sys_User>().
                        OrderBy(pagination.sidx + " " + pagination.sord).
                        Skip(pagination.rows * (pagination.page - 1)).
                        Take(pagination.rows * pagination.page);
                var count = db.Queryable<Sys_User>();

                list = data.ToList();
                records = count.Count();
                if (keyword != null && keyword != "")
                {
                    list = data.Where(c => c.F_Account.Contains(keyword) || c.F_RealName.Contains(keyword) || c.F_MobilePhone.Contains(keyword)).ToList();
                    records = count.Where(c => c.F_Account.Contains(keyword) || c.F_RealName.Contains(keyword) || c.F_MobilePhone.Contains(keyword)).Count(); ;
                }
                return list;
            }
        }

        public int RevisePassword(string id, string password)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                return db.Update<Sys_User>(new { F_Password = password }, c => c.F_Id == id).ObjToInt();
            }
        }

    }
}
