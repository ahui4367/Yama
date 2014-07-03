namespace YesHJ.Fx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Configuration;

    using YesHJ.Fx.Constant;
    using YesHJ.Fx.Error;
    using YesHJ.Fx.Ref;

    public abstract class AppEngine
    {
        #region Fields

        protected static IDictionary<string, MethodBase> g_methodCache;

        private static AppEngine g_instance;
        private static object g_locker = new object();

        #endregion Fields

        #region Properties

        public static AppEngine Current
        {
            get
            {
                if (g_instance  == null)
                {
                    lock (g_locker)
                    {
                        if (g_instance == null)
                        {
                            AppWarmUp();
                            AppEngine instance = EngineContext.Instance.Items.Get(ContextConstants.APPENGINE_REGKEY) as AppEngine;
                            Assert.ThrowIfNullOrEmpty("appengine", instance);

                            Interlocked.Exchange(ref g_instance, instance);
                        }
                    }
                }
                return g_instance;
            }
        }

        #endregion Properties

        #region Methods

        public abstract void Start();

        public abstract void Stop();

        private static void AppWarmUp()
        {
            g_methodCache = new Dictionary<string, MethodBase>();
            ScanTypes(g_methodCache);
            MethodBase method = null;
            foreach (string key in g_methodCache.Keys)
            {
                method = g_methodCache[key];
                if (method != null && method.IsStatic)
                {
                    method.Invoke(null, null);
                }
            }
        }

        private static void ScanTypes(IDictionary<string, MethodBase> cache)
        {
            cache.Clear();

            foreach (Assembly item in EngineContext.Instance.AssembliesCache)
            {
                if (item.FullName.StartsWith("System")
                    || item.FullName.StartsWith("mscor")
                    || item.FullName.StartsWith("Microsoft"))
                {
                    continue;
                }

                try
                {
                    foreach (Type type in item.GetTypes())
                    {
                        var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
                        foreach (MethodInfo method in methods)
                        {
                            var attr = method.GetCustomAttribute<WarmupAttribute>();
                            if (attr != null && !g_methodCache.ContainsKey(attr.Name))
                                g_methodCache.Add(attr.Name, method);
                        }
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        #endregion Methods
    }
}