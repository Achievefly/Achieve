using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchieveEntity;

namespace AchieveInterfaceDAL
{
    public interface ISys_LayimDAL
    {
        /// <summary>
        /// 获取好友
        /// </summary>
        /// <returns></returns>
        CSResult GetAllfriend(string userid);
        /// <summary>
        /// 添加聊天记录
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        bool AddChatrecord(Sys_Chatrecord cd);
        /// <summary>
        /// 获取群
        /// </summary>
        /// <returns></returns>
        CSResult GetAllGroup(string userid);
        /// <summary>
        /// 获取群好友
        /// </summary>
        /// <returns></returns>
        CSResult GetAllGroupFriend(string groupid);
    }
}
