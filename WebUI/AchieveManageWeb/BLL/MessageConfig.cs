using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AchieveManageWeb.BLL
{
    public class MessageConfig
    {
        /// <summary>
        /// 用户给用户发送信息的时候连接成功消息
        /// </summary>
        public const string ClientToClientConnectedSucceed = "C to C 连接服务器成功";
        public const string ClientToGroupConnectedSucceed = "C to G 连接服务器成功";
        public const string ClientTypeCTC = "one";
        public const string ClientTypeCTG = "group";
    }
}