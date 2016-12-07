using AchieveInterfaceDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace AchieveDALFactory
{
    /// <summary>
    /// 工厂类：创建访问数据库的实例对象
    /// </summary>
    public class DALFactory
    {
        /// <summary>
        /// 根据传入的类名获取实例对象
        /// </summary>
        private static object GetInstance(string name)
        {
            //ILog log = LogManager.GetLogger(typeof(Factory));  //初始化日志记录器

            string configName = System.Configuration.ConfigurationManager.AppSettings["DataAccess"];
            if (string.IsNullOrEmpty(configName))
            {
                //log.Fatal("没有从配置文件中获取命名空间名称！");   //Fatal致命错误，优先级最高
                throw new InvalidOperationException();    //抛错，代码不会向下执行了
            }

            string className = string.Format("{0}.{1}", configName, name);  //AchieveDAL.传入的类名name

            //加载程序集
            System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(configName);
            //创建指定类型的对象实例
            return assembly.CreateInstance(className);
        }


        public static ISys_UserDAL GetUserDAL()
        {
            ISys_UserDAL dal = GetInstance("Sys_UserDAL") as ISys_UserDAL;
            return dal;
        }

        public static ISys_ModuleDAL GetModuleDAL()
        {
            ISys_ModuleDAL dal = GetInstance("Sys_ModuleDAL") as ISys_ModuleDAL;
            return dal;
        }

        public static ISys_ModuleButtonDAL GetModuleButtonDAL()
        {
            ISys_ModuleButtonDAL dal = GetInstance("Sys_ModuleButtonDAL") as ISys_ModuleButtonDAL;
            return dal;
        }

        public static ISys_OrganizeDAL GetOrganizeDAL()
        {
            ISys_OrganizeDAL dal = GetInstance("Sys_OrganizeDAL") as ISys_OrganizeDAL;
            return dal;
        }

        public static ISys_AreaDAL GetAreaDAL()
        {
            ISys_AreaDAL dal = GetInstance("Sys_AreaDAL") as ISys_AreaDAL;
            return dal;
        }

        public static ISys_DutyDAL GetDutyDAL()
        {
            ISys_DutyDAL dal = GetInstance("Sys_DutyDAL") as ISys_DutyDAL;
            return dal;
        }

        public static ISys_ItemsDetailDAL GetItemsDetailDAL()
        {
            ISys_ItemsDetailDAL dal = GetInstance("Sys_ItemsDetailDAL") as ISys_ItemsDetailDAL;
            return dal;
        }

        public static ISys_ItemsDAL GetItemsDAL()
        {
            ISys_ItemsDAL dal = GetInstance("Sys_ItemsDAL") as ISys_ItemsDAL;
            return dal;
        }

        public static ISys_RoleDAL GetRoleDAL()
        {
            ISys_RoleDAL dal = GetInstance("Sys_RoleDAL") as ISys_RoleDAL;
            return dal;
        }

        public static ISys_RoleAuthorizeDAL GetRoleAuthorizeDAL()
        {
            ISys_RoleAuthorizeDAL dal = GetInstance("Sys_RoleAuthorizeDAL") as ISys_RoleAuthorizeDAL;
            return dal;
        }
    }
}
