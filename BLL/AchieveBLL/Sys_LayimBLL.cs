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
    public class Sys_LayimBLL
    {
        ISys_LayimDAL dal = DALFactory.GetLayimDAL();

        /// <summary>
        /// 获取好友
        /// </summary>
        public CSResult GetAllfriend(string userid)
        {
            return dal.GetAllfriend(userid);
        }
        /// <summary>
        /// 添加聊天记录
        /// </summary>
        public bool AddChatrecord(Sys_Chatrecord cd)
        {
            return dal.AddChatrecord(cd);
        }
        /// <summary>
        /// 获取群
        /// </summary>
        /// <returns></returns>
        public CSResult GetAllGroup(string userid)
        {
            return dal.GetAllGroup(userid);
        }

        public CSResult GetForm(string groupid)
        {
            return dal.GetAllGroupFriend(groupid);
        }
    }
}
