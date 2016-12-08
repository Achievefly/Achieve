using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchieveCommon;
using AchieveCommon.Base;
using AchieveEntity;
using AchieveInterfaceDAL;
using SqlSugar;

namespace AchieveDAL
{
    public class Sys_RoleDAL : BaseDAL<Sys_Role>, ISys_RoleDAL
    {
        public override List<Sys_Role> GetList(string id = "")
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                List<Sys_Role> list = null;
                var data = db.Queryable<Sys_Role>().Where(c=>c.F_Category == 1);
                if (id != "" && id != null)
                {
                    data = data.Where(c => c.F_Id == id);
                }
                list = data.ToList();
                if (list.Count > 0)
                {
                    return list;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool Add(Sys_Role obj, List<Sys_RoleAuthorize> list,bool isadd)
        {
            using (SqlSugarClient db = SqlSugarDao.GetInstance())//开启数据库连接
            {
                bool i = false;
                db.CommandTimeOut = 30000;//设置超时时间
                try
                {
                    db.BeginTran();//开启事务
                    if (!isadd)
                    {
                        db.Delete<Sys_RoleAuthorize>("F_ObjectId=@F_id", new { F_id = obj.F_Id });
                        string[] notstr = { "F_CreatorUserId", "F_CreatorTime", "F_Category" };
                        db.AddDisableUpdateColumns(notstr);//添加禁止更新列
                        db.Update(obj);
                    }
                    else
                    {
                        db.Insert(obj);
                    }
                    db.InsertRange(list);
                    i = true;
                    db.CommitTran();//提交事务
                }
                catch (Exception ex)
                {
                    db.RollbackTran();//回滚事务
                    i = false;
                }
                return i;
            }
        }
    }
}
