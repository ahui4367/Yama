namespace YesHJ.Fx.Extension
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class ExpressionExtension
    {
        #region Methods

        public static string GenerateSQL<TSource>(this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate)
        {
            var expr = Expression.Call(
                null,
                ((MethodInfo)MethodBase.GetCurrentMethod()).MakeGenericMethod(new Type[] { typeof(TSource) }),
                new Expression[] { source.Expression, Expression.Quote(predicate) });

            var interpreter = new QueryInterpreter();
            return interpreter.ToSQL(expr);
        }

        #endregion Methods
    }
}