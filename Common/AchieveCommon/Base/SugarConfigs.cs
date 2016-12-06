using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;

namespace AchieveCommon.Base
{
    public class SugarConfigs
    {
        //key类名 value表名(表别名设置，优先级高于 特性设置方式)
        public static List<KeyValue> MpList = new List<KeyValue>(){
            new KeyValue(){ Key="Sys_Area", Value="Sys_Area"},
            new KeyValue(){ Key="Sys_Items", Value="Sys_Items"},
            new KeyValue(){ Key="Sys_ItemsDetail", Value="Sys_ItemsDetail"},
            new KeyValue(){ Key="Sys_LogEntity", Value="Sys_LogEntity"},
            new KeyValue(){ Key="Sys_Module", Value="Sys_Module"},
            new KeyValue(){ Key="Sys_ModuleButton", Value="Sys_ModuleButton"},
            new KeyValue(){ Key="Sys_ModuleForm", Value="Sys_ModuleForm"},
            new KeyValue(){ Key="Sys_Organize", Value="Sys_Organize"},
            new KeyValue(){ Key="Sys_Role", Value="Sys_Role"},
            new KeyValue(){ Key="Sys_RoleAuthorize", Value="Sys_RoleAuthorize"},
            new KeyValue(){ Key="Sys_User", Value="Sys_User"},
            new KeyValue(){ Key="Sys_UserLogOn", Value="Sys_UserLogOn"}
            };
    }
}