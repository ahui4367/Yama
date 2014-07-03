using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesHJ.Fx.Mapping
{
    public interface IMapping<TSource, TDest>
        where TSource : class
        where TDest : class
    {
        TDest Mapping(TSource source, Func<TSource, TDest> action);

        TDest Mapping(TSource source);
    }
}
