using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesHJ.Fx.Mapping
{
    public interface IMapper
    {
        void Map<TSource, TDest>(TSource src, TDest dest)
            where TSource : class
            where TDest : class;

        IMapping<TSource, TDest> CreateMapping<TSource, TDest>()
            where TSource : class
            where TDest : class;
    }
}
