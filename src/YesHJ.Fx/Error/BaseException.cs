/***************************************************************************************
 *
 * 功能说明：异常处理基类
 *
 * 当前文件：BaseException.cs
 *
 * 作    者：Dellinger.Zhang
 *
 * 修改版本：Alpha
 *
 * @Copyright by hujiang.com
 **************************************************************************************/
namespace YesHJ.Fx.Error
{
    using System;

    public class BaseException : ApplicationException
    {
        #region Constructors

        public BaseException()
        {
        }

        public BaseException(string message)
            : base(message)
        {
        }

        public BaseException(string message, Exception innerExpception)
            : base(message, innerExpception)
        {
        }

        #endregion Constructors
    }
}