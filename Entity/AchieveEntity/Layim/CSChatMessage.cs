using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AchieveEntity
{
    public class CSChatMessage
    {
        public CSChatMessage() {
            addtime = DateTime.Now.ToString("HH:mm:ss");
        }
        /// <summary>
        /// 消息来源
        /// </summary>
        public CSUser fromuser { get; set; }
        public CSUser touser { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 消息发送时间
        /// </summary>
        public string addtime { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public CSMessageType msgtype { get; set; }

        public object other { get; set; }
    }
}