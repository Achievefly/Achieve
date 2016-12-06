using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace AchieveEntity
{
    //用户表
    public class Sys_User
    {

        /// <summary>
        /// 用户主键
        /// </summary>
        public string F_Id { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        public string F_Account { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string F_RealName { get; set; }
        /// <summary>
        /// 呢称
        /// </summary>
        public string F_NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string F_HeadIcon { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public bool? F_Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? F_Birthday { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string F_MobilePhone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string F_Email { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        public string F_WeChat { get; set; }
        /// <summary>
        /// 主管主键
        /// </summary>
        public string F_ManagerId { get; set; }
        /// <summary>
        /// 安全级别
        /// </summary>
        public int F_SecurityLevel { get; set; }
        /// <summary>
        /// 个性签名
        /// </summary>
        public string F_Signature { get; set; }
        /// <summary>
        /// 组织主键
        /// </summary>
        public string F_OrganizeId { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>
        public string F_DepartmentId { get; set; }
        /// <summary>
        /// 角色主键
        /// </summary>
        public string F_RoleId { get; set; }
        /// <summary>
        /// 岗位主键
        /// </summary>
        public string F_DutyId { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool? F_IsAdministrator { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        public int? F_SortCode { get; set; }
        /// <summary>
        /// 删除标志
        /// </summary>
        public bool? F_DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        public bool? F_EnabledMark { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string F_Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? F_CreatorTime { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        public string F_CreatorUserId { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? F_LastModifyTime { get; set; }
        /// <summary>
        /// 最后修改用户
        /// </summary>
        public string F_LastModifyUserId { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? F_DeleteTime { get; set; }
        /// <summary>
        /// 删除用户
        /// </summary>
        public string F_DeleteUserId { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string F_Password { get; set; }


    }
}

