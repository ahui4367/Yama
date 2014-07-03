/***************************************************************************************
 *
 * 功能说明：异常参数检查
 *
 * 当前文件：ParameterChecker.cs
 *
 * 作    者：Dellinger.Zhang
 *
 * 修改版本：Alpha 23:27 2012/11/5
 *
 * @Copyright by hujiang.com
 **************************************************************************************/
namespace YesHJ.Fx.Error
{
    using System;

    internal class ParameterChecker
    {
        #region Methods

        /// <summary>
        /// 检查对象是否为空
        /// </summary>
        /// <param name="method"></param>
        /// <param name="paraName"></param>
        /// <param name="paraValue"></param>
        public static void CheckNull(string method,  string paraName, object paraValue)
        {
            if (paraValue == null)
                throw new ArgumentNullException(paraName, string.Format(SR.NullFormat, method));
        }

        /// <summary>
        /// 检查string是否为空
        /// </summary>
        /// <param name="method"></param>
        /// <param name="paraName"></param>
        /// <param name="paraValue"></param>
        public static void CheckNullOrEmpty(string method, string paraName, string paraValue)
        {
            if (string.IsNullOrEmpty(paraValue))
                throw new ArgumentNullException(paraName, string.Format(SR.NullOrEmptyFormat, method));
        }

        /// <summary>
        /// 检查数组是否为空
        /// </summary>
        /// <param name="method"></param>
        /// <param name="paraName"></param>
        /// <param name="paraValue"></param>
        public static void CheckNullOrEmpty(string method, string paraName, System.Array paraValue)
        {
            CheckNullOrEmpty(method, paraName, paraValue, false);
        }

        /// <summary>
        /// 检查数组
        /// </summary>
        /// <param name="method"></param>
        /// <param name="paraName"></param>
        /// <param name="paraValue"></param>
        /// <param name="deep"></param>
        public static void CheckNullOrEmpty(string method, string paraName, System.Array paraValue, bool deep)
        {
            if (paraValue == null || paraValue.Length == 0)
                throw new ArgumentNullException(paraName, string.Format(SR.NullOrEmptyFormat, method));

            if (deep)
            {
                for (int i = 0, j = paraValue.Length; i < j; i++)
                {
                    if (paraValue.GetValue(i) == null)
                        throw new ArgumentNullException(paraName, string.Format(SR.NullFormat, method + ".Items"));
                }
            }
        }

        /// <summary>
        /// 检查参数是否在范围内
        /// </summary>
        /// <param name="method"></param>
        /// <param name="paraName"></param>
        /// <param name="paraValue"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public static void CheckRange(string method, string paraName, int paraValue, int minValue, int maxValue)
        {
            if (paraValue < minValue || paraValue > maxValue)
                throw new ArgumentOutOfRangeException(paraName, paraValue,
                    string.Format(SR.RangeFormat, method));
        }

        #endregion Methods
    }
}