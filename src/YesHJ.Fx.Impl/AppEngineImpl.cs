namespace YesHJ.Fx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using YesHJ.Fx.Constant;
    using YesHJ.Fx.Ref;

    public class AppEngineImpl : AppEngine
    {
        #region Methods

        public override void Start()
        {
        }

        public override void Stop()
        {
        }

        [Warmup("Register AppEngineImpl")]
        private static void Register()
        {
            if (EngineContext.Instance.Items.Exists(ContextConstants.APPENGINE_REGKEY))
            {
                EngineContext.Instance.Items.Remove(ContextConstants.APPENGINE_REGKEY);
            }
            EngineContext.Instance.Items.Add(ContextConstants.APPENGINE_REGKEY, new AppEngineImpl());
        }

        #endregion Methods
    }
}