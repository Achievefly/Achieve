using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SqlSugar;
namespace AchieveEntity
{
    //模块按钮
    /// <summary>
    /// 属性只作为初始化映射，SetMappingTables和SetMappingColumns可以覆盖，可按自己喜好选择
    /// </summary>
    [SugarMapping(TableName = "Sys_ModuleButton")]
    public class Sys_ModuleButton
    {

        /// <summary>
        /// 按钮主键
        /// </summary>
        public string F_Id { get; set; }
        /// <summary>
        /// 模块主键
        /// </summary>
        public string F_ModuleId { get; set; }
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
        /// 图标
        /// </summary>
        public string F_Icon { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public int? F_Location { get; set; }
        /// <summary>
        /// 事件
        /// </summary>
        public string F_JsEvent { get; set; }
        /// <summary>
        /// 连接
        /// </summary>
        public string F_UrlAddress { get; set; }
        /// <summary>
        /// 分开线
        /// </summary>
        public bool? F_Split { get; set; }
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

    }
}

