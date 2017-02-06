using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AchieveManageWeb.Models
{
    public class SingalRUser
    {
        protected string _groupName { get; set; }
        private string _connectionId { get; set; }
        /// <summary>
        /// 用户当前所在组
        /// </summary>
        public string groupname
        {
            get
            {
                return this._groupName;
            }
        }
        /// <summary>
        /// 用户当前所在connectionid
        /// </summary>
        public string connectionid
        {
            get
            {
                return this._connectionId;
            }
        }
        public SingalRUser(string groupName, string connectionId)
        {
            _groupName = groupName;
            _connectionId = connectionId;
        }
        public SingalRUser() { }
    }
    /// <summary>
    /// 用户Model
    /// </summary>
    public class CSUser : SingalRUser
    {
        public CSUser(string groupName, string connectionId) :
            base(groupName, connectionId)
        {
        }
        /// <summary>
        /// 用户id
        /// </summary>
        public int userid { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string photo { get; set; }
    }
}