/***************************************************************************************
 *
 * 功能说明：加密功能模块
 *
 * 当前文件：CryptoHelper.cs
 *
 * 作    者：Dellinger.Zhang
 *
 * 修改版本：Alpha 13:51 2012/11/20
 *
 * @Copyright by hujiang.com
 **************************************************************************************/
namespace YesHJ.Fx.Util
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public sealed class CryptoHelper
    {
        #region Fields

        private static string desKey = "yeshj2012";
        private static byte[] Keys = { 0xEF, 0xAB, 0x56, 0x78, 0x90, 0x34, 0xCD, 0x12 };

        #endregion Fields

        #region Methods

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString)
        {
            return DecryptDES(decryptString, desKey);
        }

        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        public static string EncodeSafeChar(string str, string key)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(key.Substring(0, 8)),
                IV = Encoding.ASCII.GetBytes(key.Substring(0, 8))
            };
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte num in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num);
            }
            stream.Close();
            return builder.ToString();
        }

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString)
        {
            return EncryptDES(encryptString, desKey);
        }

        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// MD5对字符串进行散列
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string EncryptMD5(string content)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] t = md5.ComputeHash(Encoding.UTF8.GetBytes(content));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// MD5对字符串进行散列(utf-16)
        /// </summary>
        /// <param name="content">要散列的字符串</param>
        /// <returns></returns>
        public static string EncryptMD5_UTF16(string content)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //替换为UTF-16，因为SQL Server nvarchar系列默认是UTF-16, 用UTF-8加密的MD5与SQL HASHBYTES计算的不一致。
            //refer to :http://blogs.msdn.com/b/qingsongyao/archive/2009/04/10/sql-server-and-utf-8-encoding-1-true-or-false.aspx
            //byte[] t = md5.ComputeHash(Encoding.UTF8.GetBytes(content));
            byte[] t = md5.ComputeHash(UnicodeEncoding.Unicode.GetBytes(content));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// ShA1加密
        /// </summary>
        public static string EncryptSHA1(string content)
        {
            byte[] StrRes = Encoding.Default.GetBytes(content);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            StrRes = iSHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in StrRes)
            {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString();
        }

        #endregion Methods
    }
}