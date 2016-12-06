using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace AchieveEntity
{
    //用户登录信息表
    public class Sys_UserLogOn
    {

        /// <summary>
        /// 用户登录主键
        /// </summary>
        public string F_Id { get; set; }
        /// <summary>
        /// 用户主键
        /// </summary>
        public string F_UserId { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string F_UserPassword { get; set; }
        /// <summary>
        /// 用户秘钥
        /// </summary>
        public string F_UserSecretkey { get; set; }
        /// <summary>
        /// 允许登录时间开始
        /// </summary>
        public DateTime F_AllowStartTime { get; set; }
        /// <summary>
        /// 允许登录时间结束
        /// </summary>
        public DateTime F_AllowEndTime { get; set; }
        /// <summary>
        /// 暂停用户开始日期
        /// </summary>
        public DateTime F_LockStartDate { get; set; }
        /// <summary>
        /// 暂停用户结束日期
        /// </summary>
        public DateTime F_LockEndDate { get; set; }
        /// <summary>
        /// 第一次访问时间
        /// </summary>
        public DateTime F_FirstVisitTime { get; set; }
        /// <summary>
        /// 上一次访问时间
        /// </summary>
        public DateTime F_PreviousVisitTime { get; set; }
        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime F_LastVisitTime { get; set; }
        /// <summary>
        /// 最后修改密码日期
        /// </summary>
        public DateTime F_ChangePasswordDate { get; set; }
        /// <summary>
        /// 允许同时有多用户登录
        /// </summary>
        public bool F_MultiUserLogin { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        public int F_LogOnCount { get; set; }
        /// <summary>
        /// 在线状态
        /// </summary>
        public bool F_UserOnLine { get; set; }
        /// <summary>
        /// 密码提示问题
        /// </summary>
        public string F_Question { get; set; }
        /// <summary>
        /// 密码提示答案
        /// </summary>
        public string F_AnswerQuestion { get; set; }
        /// <summary>
        /// 是否访问限制
        /// </summary>
        public bool F_CheckIPAddress { get; set; }
        /// <summary>
        /// 系统语言
        /// </summary>
        public string F_Language { get; set; }
        /// <summary>
        /// 系统样式
        /// </summary>
        public string F_Theme { get; set; }

    }
}

