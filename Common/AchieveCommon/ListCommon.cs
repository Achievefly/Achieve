using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AchieveCommon
{
    public static class ListCommon
    {
        #region DataSet转化为list------------------------------
        /// <summary>
        /// DataSet转化为list
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <param name="tableIndex">下标</param>
        /// <returns></returns>
        public static List<T> DataSetToList<T>(DataSet dataSet, int tableIndex)
        {
            //确认参数有效
            if (dataSet == null || dataSet.Tables.Count <= 0 || tableIndex < 0)
                return null;

            DataTable dt = dataSet.Tables[tableIndex];

            List<T> list = new List<T>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //创建泛型对象
                T _t = Activator.CreateInstance<T>();
                //获取对象所有属性
                PropertyInfo[] propertyInfo = _t.GetType().GetProperties();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    foreach (PropertyInfo info in propertyInfo)
                    {
                        //属性名称和列名相同时赋值
                        if (dt.Columns[j].ColumnName.ToUpper().Equals(info.Name.ToUpper()))
                        {
                            if (dt.Rows[i][j] != DBNull.Value)
                            {
                                info.SetValue(_t, dt.Rows[i][j], null);
                            }
                            else
                            {
                                info.SetValue(_t, null, null);
                            }
                            break;
                        }
                    }
                }
                list.Add(_t);
            }
            return list;
        }
        #endregion
        #region DataTable转化为list------------------------------
        /// <summary>
        /// DataSet转化为list
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <param name="tableIndex">下标</param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(DataTable datatable)
        {
            //确认参数有效
            if (datatable == null)
                return null;

            DataTable dt = datatable;

            List<T> list = new List<T>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //创建泛型对象
                T _t = Activator.CreateInstance<T>();
                //获取对象所有属性
                PropertyInfo[] propertyInfo = _t.GetType().GetProperties();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    foreach (PropertyInfo info in propertyInfo)
                    {
                        //属性名称和列名相同时赋值
                        if (dt.Columns[j].ColumnName.ToUpper().Equals(info.Name.ToUpper()))
                        {
                            if (dt.Rows[i][j] != DBNull.Value)
                            {
                                info.SetValue(_t, dt.Rows[i][j], null);
                            }
                            else
                            {
                                info.SetValue(_t, null, null);
                            }
                            break;
                        }
                    }
                }
                list.Add(_t);
            }
            return list;
        }
        #endregion
    }
}
