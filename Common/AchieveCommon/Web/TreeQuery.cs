using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AchieveCommon.Web
{
    public static class TreeQuery
    {
        public static List<T> TreeWhere<T>(this List<T> entityList, Predicate<T> condition, string keyValue = "F_Id", string parentId = "F_ParentId") where T : class
        {
            List<T> locateList = entityList.FindAll(condition);
            var parameter = Expression.Parameter(typeof(T), "t");
            List<T> treeList = new List<T>();
            foreach (T entity in locateList)
            {
                treeList.Add(entity);
                string pId = entity.GetType().GetProperty(parentId).GetValue(entity, null).ToString();
                while (true)
                {
                    if (string.IsNullOrEmpty(pId) && pId == "0")
                    {
                        break;
                    }
                    var upLambda = (Expression.Equal(Expression.Property(parameter,keyValue), Expression.Constant(pId)));//.ToLambda<Predicate<T>>(parameter).Compile();
                    Predicate<T> lambda = Expression.Lambda<Predicate<T>>(upLambda, parameter).Compile();
                    T upRecord = entityList.Find(lambda);
                    if (upRecord != null)
                    {
                        treeList.Add(upRecord);
                        pId = upRecord.GetType().GetProperty(parentId).GetValue(upRecord, null).ToString();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return treeList.Distinct().ToList();
        }
    }
}
