/***************************************************************************************
 *
 * 功能说明：字符串扩展方法，提供通用转换操作。
 *
 * 当前文件：String.Extension.cs
 *
 * 作    者：Dellinger.Zhang
 *
 * 修改版本：Alpha
 *
 * @Copyright by hujiang.com
 **************************************************************************************/
namespace YesHJ.Fx.Extension
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public static class StringExtension
    {
        #region Methods

        public static bool AsBoolean(this string thiz, bool defaultValue = false)
        {
            if (string.IsNullOrEmpty(thiz)) return defaultValue;

            bool result = defaultValue;

            bool.TryParse(thiz, out result);

            return result;
        }

        public static DateTime AsDateTime(this string thiz, [Optional]DateTime defaultValue)
        {
            DateTime result = defaultValue;

            DateTime.TryParse(thiz, out result);

            return result;
        }

        public static double AsDouble(this string thiz, double defaultValue = double.MinValue)
        {
            double v = defaultValue;

            double.TryParse(thiz, out v);

            return v;
        }

        public static Guid AsGuid(this string thiz, [Optional]Guid defaultValue)
        {
            Guid result = defaultValue;

            #if NET40
            Guid.TryParse(thiz, out result);
            #else
            try
            {
                result = new Guid(thiz);
            }
            catch (Exception)
            { }
            #endif

            return result;
        }

        public static int AsInteger(this string thiz, int defaultValue = int.MinValue)
        {
            int v = defaultValue;

            int.TryParse(thiz, out v);

            return v;
        }

        public static long AsLong(this string thiz, long defaultValue = long.MinValue)
        {
            long v = defaultValue;

            long.TryParse(thiz, out v);

            return v;
        }

        /// <summary>
        /// 整形数组转字符串
        /// </summary>
        /// <param name="thiz"></param>
        /// <param name="splitChar"></param>
        public static void LoadFromIntArray(this string thiz, int[] intArr, string splitChar = ",")
        {
            thiz = string.Join(splitChar, intArr);
        }

        /// <summary>
        /// 字符串转整形数组
        /// </summary>
        /// <param name="splitChar"></param>
        /// <returns></returns>
        public static int[] ParseIntArray(this string thiz, char splitChar = ',')
        {
            int[] result = new int[0];

            if (!string.IsNullOrEmpty(thiz))
            {
                List<int> data = new List<int>();
                int v = -1;
                var arr = thiz.Split(splitChar);
                foreach (var item in arr)
                {
                    if (int.TryParse(item, out v))
                    {
                        data.Add(v);
                    }
                }

                result = data.ToArray();
            }

            return result;
        }

        #endregion Methods
    }
}