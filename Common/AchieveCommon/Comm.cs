using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace AchieveCommon
{
    public class Comm
    {

        /// <summary>
        /// 输出操作按钮
        /// </summary>
        /// <param name="dt">根据用户id和菜单标识码得到的用户可以操作的此菜单下的按钮集合</param>
        /// <param name="pageName">当前页面名称，方便拼接js函数名</param>
        public static string GetToolBar(DataTable dt, string pageName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"toolbar\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (dt.Rows[i]["Code"].ToString())
                {
                    case "add":
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"Add" + pageName + "();\"},");
                        break;
                    case "edit":
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"Edit" + pageName + "();\"},");
                        break;
                    case "delete":
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"Del" + pageName + "();\"},");
                        break;
                    case "setuserrole":
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"SetUserRole();\"},");
                        break;
                    case "setuserdept":
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"SetUserDept();\"},");
                        break;
                    case "roleauthorize":
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"RoleAuthorize();\"},");
                        break;
                    case "export":
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"" + pageName + "Export();\"},");
                        break;
                    case "import":
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"" + pageName + "Import();\"},");
                        break;
                    case "setmenubutton":
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"SetMenuButton();\"},");
                        break;
                    case "expandall":
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"" + pageName + "Expandall();\"},");
                        break;
                    case "collapseall":
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"" + pageName + "Collapseall();\"},");
                        break;
                    case "seltabdata":
                        sb.Append("{\"text\": \"" + dt.Rows[i]["Name"] + "\",\"iconCls\":\"" + dt.Rows[i]["Icon"] + "\",\"handler\":\"SelTabData();\"},");
                        break;
                    default:
                        //browser不是按钮
                        break;
                }
            }

            bool flag = true;   //是否有浏览权限
            DataRow[] row = dt.Select("code = 'search'");
            if (row.Length == 0)  //没有浏览权限
            {
                flag = false;
                if (dt.Rows.Count > 0)
                    sb.Remove(sb.Length - 1, 1);
            }
            else
            {
                if (dt.Rows.Count > 1)
                    sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("],\"success\":true,");
            if (flag)
                sb.Append("\"search\":true}");
            else
                sb.Append("\"search\":false}");

            return sb.ToString();
        }

        /// <summary>
        /// 获取客户端的IP地址
        /// </summary>
        /// <returns>客户端IP地址</returns>
        public static string Get_ClientIP()
        {
            string result = string.Empty;
            result = HttpContext.Current.Request.Headers["X-Real-IP"]; //Nginx 为前端时获取IP地址的方法
            if (result != null)
                return result;

            if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)//发出请求的远程主机的IP地址
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            else if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)//判断是否设置代理，若使用了代理
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)//获取代理服务器的IP
                {
                    result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else
                {
                    result = HttpContext.Current.Request.UserHostAddress;
                }
            }
            else
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            if (result == "::1")
                result = string.Empty;
            return result;
        }

        /// <summary>
        /// 判断dataset是否为空，datatable里面是否有数据
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static bool ExistsDataSet(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static string CreatePhoneValiCode()
        {
            return new Random().Next(100000, 999999).ToString().PadLeft(6, '0');
        }

        public static string EncryptionEmailOrPhoneOrCarId(string str, int type)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(str))
            {
                switch (type)
                {
                    case 1://加密手机号码
                        if (str.Length == 11)
                            result = str.Substring(0, 3) + "****" + str.Substring(7, 4);
                        else
                            result = str.Substring(0, 2) + "***";
                        break;
                    case 2://加密邮箱
                        int at = str.IndexOf("@");
                        result = str.Substring(0, 3) + "****" + str.Substring(at, str.Length - at);
                        break;
                    case 3://加密身份证
                        if (str.Length == 15)
                            result = str.Substring(0, 3) + "****" + str.Substring(12, 3);
                        else if (str.Length == 18)
                            result = str.Substring(0, 3) + "****" + str.Substring(15, 3);
                        break;
                    default:
                        if (str.Length < 11 && str.Length > 2)
                            result = str.Substring(0, 2) + "***";
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// 动态创建数据表 固定字段 Id, CreateTime,CreateBy,UpdateTime,UpdateBy
        /// </summary>
        /// <returns></returns>
        public static bool CreateTable(string tabName)
        {
            bool res = false;
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@TabName", SqlDbType.NVarChar,50)
			    };
                parameters[0].Value = tabName;
                SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.StoredProcedure, "sp_CreateTable", parameters);
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }

        /// <summary>
        /// 动态创建数据表 固定字段 Id, CreateTime,CreateBy,UpdateTime,UpdateBy
        /// </summary>
        /// <returns></returns>
        public static bool DropTable(string tabName)
        {
            bool res = false;
            try
            {
                string sql = "DROP TABLE " + tabName;
                SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, sql, null);
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }

        /// <summary>
        /// 动态添加数据库字段 
        /// </summary>
        /// <returns></returns>
        public static bool AddTabField(string tabName, string fieldName, string dataType)
        {
            bool res = false;
            try
            {
                string sql = " ALTER TABLE " + tabName + " ADD " + fieldName + " " + dataType + " NULL ";
                SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, sql, null);
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }

        /// <summary>
        /// 动态更新数据库字段类型
        /// </summary>
        /// <returns></returns>
        public static bool UpdateTabField(string tabName, string fieldName, string dataType)
        {
            bool res = false;
            try
            {
                string sql = " ALTER TABLE " + tabName + " ALTER COLUMN " + fieldName + " " + dataType + " NULL ";
                SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, sql, null);
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }


        /// <summary>
        /// 动态删除数据库字段
        /// </summary>
        /// <returns></returns>
        public static bool DelTabField(string tabName, string fieldName)
        {
            bool res = false;
            try
            {
                string sql = " ALTER TABLE " + tabName + " DROP COLUMN " + fieldName;
                SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, sql, null);
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }

        /// <summary>
        /// 获取列Json
        /// </summary>
        /// <param name="TabId"></param>
        /// <returns></returns>
        public static string GetColumnsJsonStr(int TabId)
        {
            string fieldJson = "{\"columns\":[[{\"field\":\"Id\",\"title\":\"主键\",\"width\":\"40\"},";
            string sql = "SELECT Id,FieldName,FieldViewName FROM tbFields WHERE TabId=@TabId";
            SqlParameter[] paras = { 
                                   new SqlParameter("@TabId",SqlDbType.Int)
                                   };
            paras[0].Value = TabId;
            DataTable dtFields = SqlHelper.GetDataTable(SqlHelper.connStr, CommandType.Text, sql, paras);
            if (dtFields.Rows.Count > 0)
            {
                foreach (DataRow dr in dtFields.Rows)
                {
                    fieldJson += "{\"field\":\"" + dr["FieldName"].ToString() + "\",\"title\":\"" + dr["FieldViewName"].ToString() + "\",\"width\":\"100\"},";
                }
            }
            fieldJson += "{\"field\":\"CreateBy\",\"title\":\"创建人\",\"width\":\"80\"},";
            fieldJson += "{\"field\":\"CreateTime\",\"title\":\"创建时间\",\"width\":\"130\"},";
            fieldJson += "{\"field\":\"UpdateBy\",\"title\":\"最后更新人\",\"width\":\"80\"},";
            fieldJson += "{\"field\":\"UpdateTime\",\"title\":\"最后更新时间\",\"width\":\"130\"}";
            fieldJson += "]]}";
            return fieldJson;
        }

        /// <summary>
        /// 获取列名字符串 用于查询
        /// </summary>
        /// <param name="TabId"></param>
        /// <returns></returns>
        public static string GetColumnsStr(int TabId)
        {
            string cols = "Id,CreateBy,CreateTime,UpdateBy,UpdateTime,";
            string sql = "SELECT Id,FieldName,FieldViewName FROM tbFields WHERE TabId=@TabId";
            SqlParameter[] paras = { 
                                   new SqlParameter("@TabId",SqlDbType.Int)
                                   };
            paras[0].Value = TabId;
            DataTable dtFields = SqlHelper.GetDataTable(SqlHelper.connStr, CommandType.Text, sql, paras);
            if (dtFields.Rows.Count > 0)
            {
                foreach (DataRow dr in dtFields.Rows)
                {
                    cols += dr["FieldName"].ToString() + ",";
                }
            }
            cols = cols.TrimEnd(',');
            return cols;
        }
    }

}
