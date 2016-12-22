using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace AchieveManageWeb
{
    /// <summary>
    /// upload_json 的摘要说明
    /// </summary>
    public class upload_json : IHttpHandler
    {
        private HttpContext context;

        public void ProcessRequest(HttpContext context)
        {
            String aspxUrl = context.Request.Path.Substring(0, context.Request.Path.LastIndexOf("/") + 1);

            //文件保存目录路径
            String savePath = "/attached/";

            //文件保存目录URL
            String saveUrl = aspxUrl + "/attached/";

            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();
            extTable.Add("image", "gif,jpg,jpeg,png,bmp");
            extTable.Add("media", "swf,flv,mid,avi,mpg,asf,rm,rmvb");
            extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");
            extTable.Add("music", "mp3,wav,wma,wmv");

            //最大文件大小
            int maxSize = 1000000;
            this.context = context;

            HttpPostedFile imgFile = context.Request.Files["File"];
            if (imgFile == null)
            {
                showError("请选择文件。");
            }
            String fileName = imgFile.FileName;
            String fileExt = Path.GetExtension(fileName).ToLower();

            String dirPath = context.Server.MapPath(savePath);
            string key = null;
            foreach (System.Collections.DictionaryEntry de in extTable)
            {
                if (Array.IndexOf(de.Value.ToString().Split(','), fileExt.Substring(1).ToLower()) != -1)
                {
                    key = de.Key.ToString();//得到key
                    break;//退出foreach遍历
                }
            }
            if (!Directory.Exists(dirPath))
            {
                showError("上传目录不存在。");
            }

            String dirName = key;
            if (String.IsNullOrEmpty(dirName))
            {
                dirName = "file";
            }
            if (!extTable.ContainsKey(dirName))
            {
                showError("目录名不正确。");
            }

            

            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                showError("上传文件大小超过限制。");
            }

            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
            }

            //创建文件夹
            dirPath += dirName + "/";
            saveUrl += dirName + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            dirPath += ymd + "/";
            saveUrl += ymd + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            String filePath = dirPath + newFileName;

            imgFile.SaveAs(filePath);

            String fileUrl = saveUrl + newFileName;

            Hashtable hash = new Hashtable();
            hash["error"] = 0;
            hash["status"] = true;
            hash["message"] = "上传成功";
            hash["F_FileUrl"] = fileUrl.Substring(1);
            hash["F_FileName"] = fileName;
            hash["F_FileType"] = dirName;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(hash));
            context.Response.End();
        }

        private void showError(string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["status"] = false;
            hash["message"] = message;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(hash));
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}