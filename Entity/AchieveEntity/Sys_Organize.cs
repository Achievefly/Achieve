using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace AchieveEntity
{
    //组织表
    public class Sys_Organize
    {

        /// <summary>
        /// 组织主键
        /// </summary>
        public string F_Id { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        public string F_ParentId { get; set; }
        /// <summary>
        /// 层次
        /// </summary>
        public int? F_Layers { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string F_EnCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string F_FullName { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public string F_ShortName { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string F_CategoryId { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string F_ManagerId { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string F_TelePhone { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string F_MobilePhone { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        public string F_WeChat { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string F_Fax { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string F_Email { get; set; }
        /// <summary>
        /// 归属区域
        /// </summary>
        public string F_AreaId { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string F_Address { get; set; }
        /// <summary>
        /// 允许编辑
        /// </summary>
        public bool? F_AllowEdit { get; set; }
        /// <summary>
        /// 允许删除
        /// </summary>
        public bool? F_AllowDelete { get; set; }
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

        public List<Sys_Organize> ChildNodes { get; set; }

    }
}

