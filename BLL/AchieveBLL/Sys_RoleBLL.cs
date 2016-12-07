using AchieveCommon;
using AchieveDALFactory;
using AchieveEntity;
using AchieveInterfaceDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchieveBLL
{
    public class Sys_RoleBLL
    {
        ISys_RoleDAL dal = DALFactory.GetRoleDAL();

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        public List<Sys_Role> GetList(string id="")
        {
            return dal.GetList(id);
        }

        public Sys_Role GetForm(string id)
        {
            return dal.GetForm("Sys_Role", id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public int Add(Sys_Role obj, string[] disstr,bool isadd=true)
        {
            List<Sys_RoleAuthorize> list = new List<Sys_RoleAuthorize>();
            var module = new Sys_ModuleBLL().GetList();
            var modeulebutton = new Sys_ModuleButtonBLL().GetList();
            foreach (var item in disstr)
            {
                Sys_RoleAuthorize role = new Sys_RoleAuthorize();
                role.F_Id = System.Guid.NewGuid().ToString();
                role.F_ItemId = item;
                role.F_ObjectId = obj.F_Id;
                role.F_ObjectType = 1;
                role.F_CreatorTime = DateTime.Now;
                role.F_CreatorUserId = obj.F_CreatorUserId==null?obj.F_LastModifyUserId:obj.F_CreatorUserId;
                if (module.Find(t => t.F_Id == item) != null)
                {
                    role.F_ItemType = 1;
                }
                if (modeulebutton.Find(t => t.F_Id == item) != null)
                {
                    role.F_ItemType = 2;
                }
                list.Add(role);
            }
            return dal.Add(obj, list, isadd);
        }
        public int Delete(string idstr)
        {
            return dal.Delete(idstr);
        }
    }
}
