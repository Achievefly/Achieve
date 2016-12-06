using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SqlSugar;
namespace AchieveEntity
{
    //系统日志
    /// <summary>
    /// 属性只作为初始化映射，SetMappingTables和SetMappingColumns可以覆盖，可按自己喜好选择
    /// </summary>
    [SugarMapping(TableName = "Sys_ItemsDetail")]
    public class Sys_LogEntity
    {

        /// <summary>
        /// 日志主键
        /// </summary>
        public string F_Id { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime F_Date { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string F_Account { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string F_NickName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string F_Type { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string F_IPAddress { get; set; }
        /// <summary>
        /// IP所在城市
        /// </summary>
        public string F_IPAddressName { get; set; }
        /// <summary>
        /// 系统模块Id
        /// </summary>
        public string F_ModuleId { get; set; }
        /// <summary>
        /// 系统模块
        /// </summary>
        public string F_ModuleName { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public bool F_Result { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string F_Description { get; set; }
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

