using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace AchieveEntity
{
    //系统模块
    public class Sys_Module
    {

        /// <summary>
        /// 模块主键
        /// </summary>
        public string F_Id { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        public string F_ParentId { get; set; }
        /// <summary>
        /// 层次
        /// </summary>
        public int F_Layers { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string F_EnCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string F_FullName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string F_Icon { get; set; }
        /// <summary>
        /// 连接
        /// </summary>
        public string F_UrlAddress { get; set; }
        /// <summary>
        /// 目标
        /// </summary>
        public string F_Target { get; set; }
        /// <summary>
        /// 菜单
        /// </summary>
        public bool? F_IsMenu { get; set; }
        /// <summary>
        /// 展开
        /// </summary>
        public bool? F_IsExpand { get; set; }
        /// <summary>
        /// 公共
        /// </summary>
        public bool? F_IsPublic { get; set; }
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
        /// 创建日期
        /// </summary>
        public DateTime? F_CreatorTime { get; set; }
        /// <summary>
        /// 创建用户主键
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

        public List<Sys_Module> ChildNodes { get; set; }

    }
}

