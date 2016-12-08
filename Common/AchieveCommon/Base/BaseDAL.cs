﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            using (var db = SqlSugarDao.GetInstance())
            {
                bool i = db.Delete<T>("F_Id=@F_Id", new { F_Id = id }).ObjToBool();
                return i;
            }
        }
        public virtual bool Update(T obj, string[] disablestr = null)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                if (disablestr != null)
                {
                    db.AddDisableUpdateColumns(disablestr);//添加禁止更新列
                }
                bool i = db.Update<T>(obj).ObjToBool();
                return i;
            }
        }
        public virtual List<T> GetList(string table)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                List<T> list = new List<T>();
                list = db.SqlQuery<T>("select * from " + table).ToList<T>();
                return list;
            }
        }
        public virtual T GetForm(string table,string id)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                List<T> list = new List<T>();
                list = db.SqlQuery<T>("select * from " + table + " where F_Id=@F_Id", new {F_Id=id }).ToList<T>();
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
        public virtual T GetForm(string table, string idstr, string idvalue)
        {
            using (var db = SqlSugarDao.GetInstance())
            {
                List<T> list = new List<T>();
                list = db.SqlQuery<T>("select * from " + table + " where " + idstr + "=@F_Id", new { F_Id = idvalue }).ToList<T>();
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
    }
}
