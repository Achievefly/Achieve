using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace AchieveCommon.Base
{
    public abstract class BaseDAL<T> where T: class
    {
        public virtual bool Add(T obj,string[] disstr = null)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                if (disstr != null)
                {
                    db.DisableInsertColumns = disstr;
                }
                bool add = db.Insert<T>(obj).ObjToBool();
                string str = obj.ToJson();
                WriteLog.WriteDatalog(str,"",add,"新增操作","新增");
                return add;
            }
        }
        public virtual bool Add(List<T> obj, string[] disstr = null)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                if (disstr != null)
                {
                    db.DisableInsertColumns = disstr;
                }
                bool add = db.InsertRange(obj).ObjToBool();
                string str = obj.ToJson();
                WriteLog.WriteDatalog(str, "", add, "批量新增操作","新增");
                return add;
            }
        }
        public virtual bool Delete(string[] idstr)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                bool i = db.Delete<T, string>(idstr).ObjToBool();
                return i;
            }
        }
        public virtual bool Delete(string id)
        {
            string str = GetUpjsonstr(id);
            using (var db = SqlSugarDao.GetInstance())
            {
                bool i = db.Delete<T>("F_Id=@F_Id", new { F_Id = id }).ObjToBool();
                WriteLog.WriteDatalog(str, "", i, "删除操作","删除");
                return i;
            }
        }
        public virtual bool Update(T obj, string[] disablestr = null)
        {
            var otherProp = typeof(T).GetProperty("F_Id");
            var valFirst = otherProp.GetValue(obj, null) as IComparable;
            string str = GetUpjsonstr(valFirst.ToString());
            using (var db = SqlSugarDao.GetInstance())
            {
                if (disablestr != null)
                {
                    db.AddDisableUpdateColumns(disablestr);//添加禁止更新列
                }
                bool i = db.Update<T>(obj).ObjToBool();
                WriteLog.WriteDatalog(str, obj.ToJson(), i, "修改操作","修改");
                return i;
            }
        }
        public virtual List<T> GetList()
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                List<T> list = new List<T>();
                list = db.SqlQuery<T>("select * from " + GetTabelname()).ToList<T>();
                return list;
            }
        }
        public virtual T GetForm(string id)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                List<T> list = new List<T>();
                list = db.SqlQuery<T>("select * from " + GetTabelname() + " where F_Id=@F_Id", new {F_Id=id }).ToList<T>();
                if (list.Count > 0)
                {
                    return list[0];
                }
                else
                {
                    return null;
                }
            }
        }
        public virtual T GetForm(string idstr, string idvalue)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                List<T> list = new List<T>();
                list = db.SqlQuery<T>("select * from " + GetTabelname() + " where " + idstr + "=@F_Id", new { F_Id = idvalue }).ToList<T>();
                if (list.Count > 0)
                {
                    return list[0];
                }
                else
                {
                    return null;
                }
            }
        }

        public string GetUpjsonstr(string id)
        {
            
            string str = "";
            using (var db = SqlSugarDao.GetInstance())
            {
                 str = db.SqlQueryJson("select * from " + GetTabelname() + " where F_Id='" + id + "'");
            }
            return str;
        }
        public string GetTabelname()
        {
            Type objType = typeof(T);
            string tableName = "";
            //取类上的自定义特性
            object[] objs = objType.GetCustomAttributes(typeof(SugarMappingAttribute), true);
            foreach (object obj in objs)
            {
                if (obj is SugarMappingAttribute)
                {
                    SugarMappingAttribute attr = obj as SugarMappingAttribute;
                    if (attr != null)
                    {
                        tableName = attr.TableName;//表名只有获取一次
                        break;
                    }
                }
            }
            if (tableName == "" || tableName == null)
            {
                tableName = objType.Name;
            }
            return tableName;
        }
    }
}
