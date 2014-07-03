/***************************************************************************************
 *
 * 功能说明：通用服务器基类
 *
 * 当前文件：Server.cs
 *
 * 作    者：Dellinger.Zhang
 *
 * 修改版本：Alpha
 *
 * @Copyright by hujiang.com
 **************************************************************************************/
namespace YesHJ.Fx.Net
{
    using System;

    public abstract class Server : IDisposable
    {
        #region Methods

        public abstract void Dispose();

        public abstract void Reload();

        public abstract void Start();

        public abstract void Stop();

        #endregion Methods
    }
}