using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchieveCommon;
using AchieveCommon.Base;
using AchieveEntity;
using AchieveInterfaceDAL;
using SqlSugar;

namespace AchieveDAL
{
    public class Sys_LayimDAL : ISys_LayimDAL
    {
        /// <summary>
        /// 获取好友
        /// </summary>
        public CSResult GetAllfriend(string userid)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                var friendGroup = new List<CSGroupResult>();
                

                var group = db.Queryable<Sys_Relation>().Where(s1=>s1.F_UserId==userid&& s1.F_Type == 1).GroupBy(s1 => s1.F_Group).Select("F_Group").ToDataTable();
                for (int i = 0; i < group.Rows.Count; i++)
                {
                    var friends = new List<CSBaseModel>();
                    string gr = group.Rows[i]["F_Group"].ToString();
                    var fri = db.Queryable<Sys_Relation>().
                                JoinTable<Sys_User>((s1, s2) => s1.F_FriendId == s2.F_Id).
                                Where(s1 => s1.F_UserId == userid && s1.F_Type == 1).
                                Where(s1 => s1.F_Group == gr).Select("s1.*,s2.*").ToDataTable();
                    for (int j = 0; j < fri.Rows.Count; j++)
                    {
                        friends.Add(new CSFriend { id = fri.Rows[j]["F_FriendId"].ToString(), name = fri.Rows[j]["F_RealName"].ToString(), face = fri.Rows[j]["F_HeadIcon"].ToString() });
                    }
                    friendGroup.Add(new CSGroupResult { id = 1 + i, item = friends, name = group.Rows[i]["F_Group"].ToString() });
                }
                    
                
                CSResult result = new CSResult
                {
                    msg = "ok",
                    status = 1,
                    data = friendGroup
                };
                return result;
            }

        }
        /// <summary>
        /// 添加聊天记录
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        public bool AddChatrecord(Sys_Chatrecord cd)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                bool add = db.Insert<Sys_Chatrecord>(cd).ObjToBool();
                return add;
            }
        }
        /// <summary>
        /// 获取群组
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public CSResult GetAllGroup(string userid)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                var friendGroup = new List<CSGroupResult>();


                var group = db.Queryable<Sys_Relation>().Where(s1 => s1.F_FriendId == userid && s1.F_Type == 2).GroupBy(s1 => s1.F_Group).Select("F_Group").ToDataTable();
                for (int i = 0; i < group.Rows.Count; i++)
                {
                    var friends = new List<CSBaseModel>();
                    string gr = group.Rows[i]["F_Group"].ToString();
                    var fri = db.Queryable<Sys_Relation>().
                                JoinTable<Sys_Flock>((s1, s2) => s1.F_UserId == s2.F_Id).
                                Where(s1 => s1.F_FriendId == userid && s1.F_Type == 2 && s1.F_Group == gr).
                                Select("s1.*,s2.*").ToDataTable();
                    for (int j = 0; j < fri.Rows.Count; j++)
                    {
                        friends.Add(new CSFriend { id = fri.Rows[j]["F_UserId"].ToString(), name = fri.Rows[j]["F_FlockName"].ToString(), face = fri.Rows[j]["F_Flockphotos"].ToString() });
                    }
                    friendGroup.Add(new CSGroupResult { id = 1 + i, item = friends, name = group.Rows[i]["F_Group"].ToString() });
                }


                CSResult result = new CSResult
                {
                    msg = "ok",
                    status = 1,
                    data = friendGroup
                };
                return result;
            }
        }
        public CSResult GetAllGroupFriend(string groupid)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                var friends = new List<CSBaseModel>();
                var fri = db.Queryable<Sys_Relation>().
                                JoinTable<Sys_User>((s1, s2) => s1.F_FriendId == s2.F_Id).
                                Where(s1 => s1.F_UserId == groupid && s1.F_Type == 2).
                                Select("s1.*,s2.*").ToDataTable();
                for (int j = 0; j < fri.Rows.Count; j++)
                {
                    friends.Add(new CSFriend { id = fri.Rows[j]["F_FriendId"].ToString(), name = fri.Rows[j]["F_RealName"].ToString(), face = fri.Rows[j]["F_HeadIcon"].ToString() });
                }
                CSResult result = new CSResult
                {
                    msg = "ok",
                    status = 1,
                    data = friends
                };
                return result;
            }
        }
    }
}
