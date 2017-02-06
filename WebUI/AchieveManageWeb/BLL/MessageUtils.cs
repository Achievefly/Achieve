
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using AchieveManageWeb.Models;

namespace AchieveManageWeb.BLL
{
    /// <summary>
    /// 聊天工具帮助类
    /// </summary>
    public class MessageUtils
    {
        /// <summary>
        /// 根据两个用户ID得到对应的组织名称
        /// </summary>
        /// <param name="sendid">发送人（主动联系人）</param>
        /// <param name="receiveid">接收人（被动联系人）</param>
        /// <returns></returns>
        public static string GetGroupName(string sendid, string receiveid)
        {
            /*
                排序的目的就是为了保证，无论谁连接服务器，都能得到正确的组织ID
            */
            int compareResult = string.Compare(sendid, receiveid);
            if (compareResult > 0) {
                //重新排序 如果sendid>receiveid
                return string.Format("G{0}{1}", receiveid, sendid);
            }
            return string.Format("G{0}{1}", sendid, receiveid);
        }

        /// <summary>
        /// 连接组织，加上前缀，防止和单聊冲突
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public static string GetGroupName(string groupid)
        {
            return string.Format("GROUP_{0}", groupid);
        }
        #region 消息处理

        public static CSUser GetSystemUser(string groupName)
        {
            return new CSUser(groupName,"")
            {
                photo = "/photos/sys.png",
                userid = 0,
                username = "系统提示"
            };
        }
        /// <summary>
        /// 获取系统消息
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static CSChatMessage GetSystemMessage(string groupName, string msg,object other =null)
        {
            CSChatMessage chatMsg = new CSChatMessage
            {
                fromuser = GetSystemUser(groupName),
                msg = msg,
                msgtype = CSMessageType.System,
                other = other
            };
            return chatMsg;
        }

        public static List<CSChatMessage> GetHistoryMessage(string sendid,string receiveid)
        {
            string groupName = GetGroupName(sendid, receiveid);
            List<CSChatMessage> historys = new List<CSChatMessage>();
            //这里历史记录作为demo使用，可以从数据库或者缓存读取
            historys.Add(new CSChatMessage
            {
                fromuser = new CSUser(groupName, null)
                {
                    photo = "/photos/000.jpg",
                    userid = int.Parse(sendid),
                    username = "发送方的名字"
                },
                msg = "这一条是历史记录",
                msgtype = CSMessageType.Custom,
                touser = new CSUser(groupName, null)
                {
                    photo = "/photos/001.jpg",
                    userid = int.Parse(receiveid),
                    username = "接收方的名字"
                },
                other = new { t = MessageConfig.ClientTypeCTC }//这里不要忘了加t参数
            });
            historys.Add(new CSChatMessage
            {
                touser = new CSUser(groupName, null)
                {
                    photo = "/photos/000.jpg",
                    userid = int.Parse(sendid),
                    username = "发送方的名字"
                },
                msg = "这一条是历史记录",
                msgtype = CSMessageType.Custom,
                fromuser = new CSUser(groupName, null)
                {
                    photo = "/photos/001.jpg",
                    userid = int.Parse(receiveid),
                    username = "接收方的名字"
                },
                other = new { t = MessageConfig.ClientTypeCTC }//这里不要忘了加t参数
            });
            historys.Add(new CSChatMessage
            {
                fromuser = new CSUser(groupName, null)
                {
                    photo = "/photos/000.jpg",
                    userid = int.Parse(sendid),
                    username = "发送方的名字"
                },
                msg = "这一条是历史记录",
                msgtype = CSMessageType.Custom,
                touser = new CSUser(groupName, null)
                {
                    photo = "/photos/001.jpg",
                    userid = int.Parse(receiveid),
                    username = "接收方的名字"
                },
                other = new { t = MessageConfig.ClientTypeCTC }//这里不要忘了加t参数
            });
            return historys;
        }

        #endregion

        /// <summary>
        /// 序列化方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ScriptSerialize<T>(T t)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(t);
        }
    }
}