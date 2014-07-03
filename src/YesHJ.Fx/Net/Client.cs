namespace YesHJ.Fx.Net
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class Client : IDisposable
    {
        #region Methods

        public abstract void Connect(string svr, int port);

        public abstract void Disconnect();

        public abstract void Dispose();

        #endregion Methods
    }
}