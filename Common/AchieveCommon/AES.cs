using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace AchieveCommon
{
    /// <summary>
    /// 对称加密类
    /// </summary>
    public class AES
    {
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="strDecrypt"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string Decrypt(string strDecrypt, string strKey)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(strKey, "md5"));
                byte[] inputBuffer = Convert.FromBase64String(strDecrypt);
                byte[] buffer3 = null;
                using (RijndaelManaged managed = new RijndaelManaged())
                {
                    managed.Key = bytes;
                    managed.Mode = CipherMode.ECB;
                    managed.Padding = PaddingMode.PKCS7;
                    buffer3 = managed.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                }
                return Encoding.UTF8.GetString(buffer3);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="strDecrypt"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt, string key, string iv)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(key);
            byte[] buffer2 = Encoding.UTF8.GetBytes(iv);
            byte[] inputBuffer = Convert.FromBase64String(toDecrypt);
            RijndaelManaged managed = new RijndaelManaged
            {
                Key = bytes,
                IV = buffer2,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.Zeros
            };
            byte[] buffer4 = managed.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return Encoding.UTF8.GetString(buffer4);
        }

        public static string DecryptStr(string EncryptString)
        {
            string str = "";
            if (!string.IsNullOrEmpty(EncryptString))
            {
                string sSource = Decrypt(EncryptString, "cn.solefu");
                if (Utility.Left(sSource, 3) == "gk_")
                {
                    str = sSource.Substring(3);
                }
            }
            return str;
        }

        public static string DecryptStrByCBC(string EncryptString)
        {
            string str = "";
            if (!string.IsNullOrEmpty(EncryptString))
            {
                string sSource = Decrypt(EncryptString, "cn.solefu", "cn.solefu");
                if (Utility.Left(sSource, 3) == "gk_")
                {
                    str = sSource.Substring(3);
                }
            }
            return str;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="strEncrypt"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string Encrypt(string strEncrypt, string strKey)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(strKey, "md5"));
                byte[] inputBuffer = Encoding.UTF8.GetBytes(strEncrypt);
                byte[] inArray = null;
                using (RijndaelManaged managed = new RijndaelManaged())
                {
                    managed.Key = bytes;
                    managed.Mode = CipherMode.ECB;
                    managed.Padding = PaddingMode.PKCS7;
                    inArray = managed.CreateEncryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                }
                return Convert.ToBase64String(inArray, 0, inArray.Length);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="strEncrypt"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, string key, string iv)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(key);
            byte[] buffer2 = Encoding.UTF8.GetBytes(iv);
            byte[] inputBuffer = Encoding.UTF8.GetBytes(toEncrypt);
            RijndaelManaged managed = new RijndaelManaged
            {
                Key = bytes,
                IV = buffer2,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.Zeros
            };
            byte[] inArray = managed.CreateEncryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return Convert.ToBase64String(inArray, 0, inArray.Length);
        }

        public static string EncryptStr(string SourceString)
        {
            return Encrypt("gk_" + SourceString, "cn.solefu");
        }

        public static string EncryptStr(string SourceString, bool UseInUrl)
        {
            return HttpUtility.UrlEncode(EncryptStr(SourceString));
        }

        public static string EncryptStrByCBC(string SourceString)
        {
            return Encrypt("gk_" + SourceString, "cn.solefu", "cn.solefu");
        }

        public static string EncryptStrByCBC(string SourceString, bool UseInUrl)
        {
            return HttpUtility.UrlEncode(EncryptStrByCBC(SourceString));
        }
    }

}
