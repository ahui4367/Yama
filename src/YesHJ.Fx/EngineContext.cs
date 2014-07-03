namespace YesHJ.Fx
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Threading;

    using YesHJ.Fx.Caching;
    using YesHJ.Fx.Pattern;
    using YesHJ.Fx.Utils.IO;

    public class EngineContext : Singleton<EngineContext>
    {
        #region Fields

        private static readonly object g_locker = new object();
        private static readonly Guid _appId = Guid.NewGuid();

        static List<Assembly> g_assemblies;
        private static ICache<string, object> _items;

        #endregion Fields

        #region Constructors

        private EngineContext()
        {
            if (_items == null)
            {
                lock (g_locker)
                {
                    if (_items == null)
                    {
                        _items = CacheProvider.Instance.GetCache<string, object>(CacheProvider.SIMPLE_CACHE);
                    }
                }
            }
        }

        #endregion Constructors

        #region Properties

        public Guid ApplicationId
        {
            get
            {
                return _appId;
            }
        }

        public List<Assembly> AssembliesCache
        {
            get
            {
                if (g_assemblies == null)
                {
                    lock (g_locker)
                    {
                        if (g_assemblies == null)
                        {
                            List<Assembly> tempCache = new List<Assembly>();
                            var files = DirHelper.GetFiles(DirHelper.GetPhysicalPath("~/bin"), "*.dll");
                            foreach (string file in files)
                            {
                                try
                                {
                                    tempCache.Add(Assembly.LoadFile(file));
                                }
                                catch (Exception)
                                {
                                    //TODO: Add log here.
                                }
                            }
                            Interlocked.Exchange(ref g_assemblies, tempCache);
                        }
                    }
                }
                return g_assemblies;
            }
        }

        public ICache<string, object> Items
        {
            get
            {
                return _items;
            }
        }

        #endregion Properties
    }
}