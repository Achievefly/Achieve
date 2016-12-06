using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace AchieveEntity
{
    //角色授权表
    public class Sys_RoleAuthorize
    {

        /// <summary>
        /// 角色授权主键
        /// </summary>
        public string F_Id { get; set; }
        /// <summary>
        /// 项目类型1-模块2-按钮3-列表
        /// </summary>
        public int F_ItemType { get; set; }
        /// <summary>
        /// 项目主键
        /// </summary>
        public string F_ItemId { get; set; }
        /// <summary>
        /// 对象分类1-角色2-部门-3用户
        /// </summary>
        public int F_ObjectType { get; set; }
        /// <summary>
        /// 对象主键
        /// </summary>
        public string F_ObjectId { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        public int F_SortCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime F_CreatorTime { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        public string F_CreatorUserId { get; set; }

    }
}

