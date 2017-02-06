
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AchieveManageWeb.Models;

namespace AchieveManageWeb.BLL
{
    public class DBHelper
    {
        private static string[] names = new string[] { "痴玉", "书筠", "诗冬", "飞枫", "盼玉", "靖菡", "宛雁", "之卉", "凡晴", "书枫", "沛梦" };
        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <returns></returns>
        public static CSResult GetFriends()
        {
            var friends = new List<CSBaseModel>();
            for (int i = 0; i < 9; i++) {
                friends.Add(new CSFriend { id = i + 10000, name = names[i], face = "/photos/00" + i + ".jpg" });
            }

            var friendGroup = new List<CSGroupResult>();

            friendGroup.Add(new CSGroupResult { id = 1, item = friends, name = "C#研发后台组" });
            //friendGroup.Add(new CSGroupResult { id = 2, item = friends, name = "JS研发前端组" });
            //friendGroup.Add(new CSGroupResult { id = 3, item = friends, name = "IOS研发移动组" });

            CSResult result = new CSResult
            {
                msg = "ok",
                status = 1,
                data = friendGroup
            };
            return result;
        }

        /// <summary>
        /// 获取分组列表
        /// </summary>
        /// <returns></returns>
        public static CSResult GetGroup()
        {
            var groups = new List<CSBaseModel>();
            for (int i = 0; i < 3; i++)
            {
                groups.Add(new CSGroup { id = i, name = "分组" + i, face = "/photos/00" + i + ".jpg" });
            }

            var friendGroup = new List<CSGroupResult>();

            friendGroup.Add(new CSGroupResult { id = 1, item = groups, name = "分组名称一" });
            friendGroup.Add(new CSGroupResult { id = 2, item = groups, name = "分组名称二" });
            friendGroup.Add(new CSGroupResult { id = 3, item = groups, name = "分组名称三"});

            CSResult result = new CSResult
            {
                msg = "ok",
                status = 1,
                data = friendGroup
            };
            return result;
        }

        /// <summary>
        /// 获取历史记录
        /// </summary>
        /// <returns></returns>
        public static CSResult GetChatLog()
        {
            CSResult result = new CSResult
            {
                msg = "ok",
                status = 1,
                data = new List<CSChatLog> {
                    new CSChatLog { id=1, name="小王八", face="/photos/001.jpg", time="10:23" },
                    new CSChatLog { id=1, name="小绿豆", face="/photos/002.jpg", time="2016-1-20" },
                    new CSChatLog { id=1, name="小毛驴", face="/photos/003.jpg", time="1-19 22:26" },
                }
            };
            return result;
        }

        /// <summary>
        /// 群组成员，需要根据群id获取成员
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static CSResult GetGroupMember(int groupId = 0)
        {
            var friends = new List<CSBaseModel>();
            for (int i = 0; i < 9; i++)
            {
                friends.Add(new CSFriend { id = i, name = "好友" + i, face = "/photos/00" + i + ".jpg" });
            }
            CSResult result = new CSResult
            {
                msg = "ok",
                status = 1,
                data = friends
            };
            return result;
        }

        /// <summary>
        /// 在封装一层业务，根据type返回不同的结果
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static CSResult GetResult(string type)
        {
            CSResult result = null;
            switch (type)
            {
                case "friend":
                    result = DBHelper.GetFriends();
                    break;
                case "group":
                    result = DBHelper.GetGroup();
                    break;
                case "log":
                    result = DBHelper.GetChatLog();
                    break;
                case "groups":
                    result = DBHelper.GetGroupMember();
                    break;
                default:
                    result = new CSResult { status = 0, data = null, msg = "无效的请求类型" };
                    break;
            }
            return result;
        }
    }
}