using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace YesHJ.Fx.Mapping
{
    public class MappingImpl<TSource, TDest> : IMapping<TSource, TDest>
        where TSource : class
        where TDest : class
    {
        private IMappingExpression<TSource, TDest> expr;

        public MappingImpl()
        {
            expr = Mapper.CreateMap<TSource, TDest>();
        }

        public TDest Mapping(TSource source, Func<TSource, TDest> action)
        {
            //TDest dest = expr
            return (TDest)null;
        }

        public TDest Mapping(TSource source)
        {
            return Mapper.Map<TSource, TDest>(source);
        }
    }
}
