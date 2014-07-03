/***************************************************************************************
 *
 * 功能说明：通用异常处理类
 *
 * 当前文件：ExceptionManager.cs
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
    using System.Diagnostics;

    /// <summary>
    /// 提供通用异常处理的常用方法
    /// </summary>
    public static class Assert
    {
        #region Delegates

        /// <summary>
        /// 创建异常委托
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public delegate T CreateExceptionDelegate<T>();

        #endregion Delegates

        #region Methods

        public static void Throw<TException>()
            where TException : Exception
        {
            Throw<TException>(null, string.Empty);
        }

        /// <summary>
        /// 按照指定格式抛出异常
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="format">格式输出字符串</param>
        /// <param name="paramters">相应的参数</param>
        public static void Throw<TException>(string format, params object[] parameters)
            where TException : Exception
        {
            Throw<TException>(null, format, parameters);
        }

        /// <summary>
        /// 按照指定格式抛出异常
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="innerException"></param>
        /// <param name="format">格式输出字符串</param>
        /// <param name="parameters">相应的参数</param>
        public static void Throw<TException>(Exception innerException,
            string format,
            params object[] parameters)
            where TException : Exception
        {
            Throw<TException>(innerException, string.Format(format, parameters));
        }

        /// <summary>
        /// 按照指定格式抛出异常
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="innerException"></param>
        /// <param name="message">输出消息</param>
        public static void Throw<TException>(Exception innerException,
            string message)
            where TException : Exception
        {
            throw Activator.CreateInstance(typeof(TException),
                    message,
                    innerException) as TException;
        }

        /// <summary>
        /// 如果condition为真时，则调用委托.
        /// </summary>
        /// <param name="condition">判断条件</param>
        /// <param name="onCreate">创建委托</param>
        public static void ThrowIf(bool condition, CreateExceptionDelegate<Exception> onCreate)
        {
            if (condition)
            {
                onCreate();
            }
        }

        /// <summary>
        /// 如果condition为真，则调用Action
        /// </summary>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="action">The action.</param>
        public static void ThrowIf(bool condition, Action action)
        {
            if (condition)
            {
                action();
            }
        }

        /// <summary>
        /// 如果参数为空，则抛出异常，自动加入调用方的方法。
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="parameter"></param>
        public static void ThrowIfNullOrEmpty(string paramName, object parameter)
        {
            var trace = new StackTrace(false);
            if (trace.FrameCount > 0)
            {
                string method = trace.GetFrame(1).GetMethod().Name;
                ThrowIfNullOrEmpty(method, paramName, parameter);
            }
        }

        /// <summary>
        /// 判断参数
        /// </summary>
        /// <param name="method"></param>
        /// <param name="paramName"></param>
        /// <param name="parameter"></param>
        public static void ThrowIfNullOrEmpty(string method, string paramName, object parameter)
        {
            ThrowIf(IsNullOrEmpty(parameter), () =>
            {
                if (parameter is string)
                {
                    ParameterChecker.CheckNullOrEmpty(method, paramName, (string)parameter);
                }
                else
                {
                    ParameterChecker.CheckNull(method, paramName, parameter);
                }
            });
        }

        /// <summary>
        /// 判断参数是否为空
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private static bool IsNullOrEmpty(object parameter)
        {
            bool isTrue = false;

            if (parameter is string)
            {
                isTrue = string.IsNullOrEmpty((string)parameter);
            }
            else
            {
                isTrue = parameter == null;
            }

            return isTrue;
        }

        #endregion Methods
    }
}