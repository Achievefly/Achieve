using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;
using AchieveManageWeb.Models;
using AchieveEntity;
using AchieveCommon.Operator;
using AchieveBLL;
using AchieveCommon;

namespace AchieveManageWeb
{
    [HubName("csHub")]
    public class CustomServiceHub : Hub
    {
        /// <summary>
        /// 当前连接ID
        /// </summary>
        private string CurrentUserConnectionId
        {
            get
            {
                return Context.ConnectionId;
            }
        }
        public Task Join()
        {
            return Clients.All.receiveMessage("某某人加入了");
        }

        /// <summary>
        /// 人对人聊天 连接服务器
        /// </summary>
        /// <param name="sendid">发送人</param>
        /// <param name="receiveid">接收人</param>
        /// <returns></returns>
        public Task ClientToClient(string sendid, string receiveid)
        {
            if (sendid == null || receiveid == null) { throw new ArgumentNullException("sendid or receiveid can't be null"); }
            //获取组名
            string groupName = MessageUtils.GetGroupName(sendid, receiveid);
            //将当前用户添加到此组织内
            Groups.Add(CurrentUserConnectionId, groupName);
            //构建系统连接成功消息
            //读取历史记录 --- 2016-3-7 修改
            var historyMsg = MessageUtils.GetHistoryMessage(sendid, receiveid);

            var msg = MessageUtils.GetSystemMessage(groupName, "连接成功", new { t = "one", currentid = sendid, receiveid = receiveid,history = historyMsg });
            //将消息推送到当前组 （A和B聊天的组） 同样调用receiveMessage方法
           
            return Clients.Caller.receiveMessage(msg);
        }

        /// <summary>
        /// 人对组聊天，连接服务器
        /// </summary>
        /// <param name="sendid">当前用户id</param>
        /// <param name="groupid">当前群组id</param>
        /// <returns></returns>
        public Task ClientToGroup(string sendid, string groupid)
        {
            if (sendid == null || groupid == null) { throw new ArgumentNullException("sendid or groupid can't be null"); }
            string groupName = MessageUtils.GetGroupName(groupid);
            Groups.Add(CurrentUserConnectionId, groupName);
            //构建系统连接成功消息
            var msg = MessageUtils.GetSystemMessage(groupName, "链接成功", new { t = "group", currentid = sendid, receiveid = groupid });
            //将消息推送到当前组 （A和B聊天的组） 同样调用receiveMessage方法
            return Clients.Caller.receiveMessage(msg);
        }

        /// <summary>
        /// 发送消息 ，服务器接收的是CSChatMessage实体，他包含发送人，接收人，消息内容等信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Task ClientSendMsgToClient(AchieveEntity.CSChatMessage msg)
        {
            var groupName = MessageUtils.GetGroupName(msg.fromuser.userid.ToString(), msg.touser.userid.ToString());
            /*
            中间处理一下消息直接转发给（A,B所在组织，即聊天窗口）
            */
            msg.other = new { t = "one" };
            msg.msgtype = AchieveEntity.CSMessageType.Custom;//消息类型为普通消息
            //添加消息记录
            Sys_Chatrecord cd = new Sys_Chatrecord();
            cd.F_Id = System.Guid.NewGuid().ToString();
            cd.F_Type = 1;
            cd.F_SendId = msg.fromuser.userid.ToString();
            cd.F_Message = msg.msg;
            cd.F_AcceptId = msg.touser.userid.ToString();
            cd.F_CreatorTime = DateTime.Now;
            cd.F_CreatorUserId = OperatorProvider.Provider.GetCurrent().UserCode;
            new Sys_LayimBLL().AddChatrecord(cd);
            return Clients.Group(groupName).receiveMessage(msg);
        }

        /// <summary>
        /// 发送消息 （群组），服务器接收的是CSChatMessage实体，他包含发送人，接收人，消息内容等信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Task ClientSendMsgToGroup(AchieveEntity.CSChatMessage msg)
        {
            //获取要推送的组织名称
            var groupName = MessageUtils.GetGroupName(msg.touser.userid.ToString());
            //附加信息，为群信息
            msg.other = new { t = "group" };
            //普通信息类型
            msg.msgtype = AchieveEntity.CSMessageType.Custom;
            //添加消息记录
            Sys_Chatrecord cd = new Sys_Chatrecord();
            cd.F_Id = System.Guid.NewGuid().ToString();
            cd.F_Type = 2;
            cd.F_SendId = msg.fromuser.userid.ToString();
            cd.F_Message = msg.msg;
            cd.F_AcceptId = msg.touser.userid.ToString();
            cd.F_CreatorTime = DateTime.Now;
            cd.F_CreatorUserId = OperatorProvider.Provider.GetCurrent().UserCode;
            new Sys_LayimBLL().AddChatrecord(cd);
            return Clients.Group(groupName).receiveMessage(msg);
        }
    }
}