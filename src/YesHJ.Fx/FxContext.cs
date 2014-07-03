using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesHJ.Fx.Caching;
using YesHJ.Fx.Pattern;

namespace YesHJ.Fx
{
    public class FxContext : Singleton<FxContext>
    {
        private FxContext() 
        {
            _items = CacheProvider.Instance.GetCache<string, object>(CacheProvider.SIMPLE_CACHE);
        }

        private ICache<string, object> _items;
        public ICache<string, object> Items
        {
            get
            {
                return _items;
            }
        }
    }
}
