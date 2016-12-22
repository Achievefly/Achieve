using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SqlSugar;
namespace AchieveEntity
{
    //行政区域表
    /// <summary>
    /// 属性只作为初始化映射，SetMappingTables和SetMappingColumns可以覆盖
    /// </summary>
    [SugarMapping(TableName = "Sys_File")]//表别名设置，会被
    public class Sys_File
    {

        /// <summary>
        /// 主键
        /// </summary>
        public string F_Id { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        public string F_ParentId { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string F_FileType { get; set; }
        /// <summary>
        /// 文件地址
        /// </summary>
        public string F_FileUrl { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string F_FileName { get; set; }
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

    }
}

