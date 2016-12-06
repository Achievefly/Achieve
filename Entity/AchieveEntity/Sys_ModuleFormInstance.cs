using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SqlSugar;
namespace AchieveEntity
{
    //模块表单实例
    /// <summary>
    /// 属性只作为初始化映射，SetMappingTables和SetMappingColumns可以覆盖，可按自己喜好选择
    /// </summary>
    [SugarMapping(TableName = "Sys_ModuleFormInstance")]
    public class Sys_ModuleFormInstance
    {

        /// <summary>
        /// 表单实例主键
        /// </summary>
        public string F_Id { get; set; }
        /// <summary>
        /// 表单主键
        /// </summary>
        public string F_FormId { get; set; }
        /// <summary>
        /// 对象主键
        /// </summary>
        public string F_ObjectId { get; set; }
        /// <summary>
        /// 表单实例Json
        /// </summary>
        public string F_InstanceJson { get; set; }
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

