using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace AchieveCommon.Base
{
    public abstract class BaseDAL<T> where T: class
    {
        public virtual int Add(T obj,string[] disstr = null)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                if (disstr != null)
                {
                    db.DisableInsertColumns = disstr;
                }
                int add = db.Insert<T>(obj).ObjToInt();
                return add;
            }
        }
        public virtual int Add(List<T> obj, string[] disstr = null)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                if (disstr != null)
                {
                    db.DisableInsertColumns = disstr;
                }
                int add = db.InsertRange(obj).ObjToInt();
                return add;
            }
        }
        public virtual int Delete(string[] idstr)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                int i = db.Delete<T, string>(idstr).ObjToInt();
                return i;
            }
        }
        public virtual int Delete(string id)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                int i = db.Delete<T>("F_Id=@F_Id", new {F_id=id }).ObjToInt();
                return i;
            }
        }
        public virtual int Update(T obj, string[] disablestr=null)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                if (disablestr != null)
                {
                    db.AddDisableUpdateColumns(disablestr);//添加禁止更新列
                }
                int i = db.Update<T>(obj).ObjToInt();
                return i;
            }
        }
    }
}
