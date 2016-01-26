using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpTreeShowoff
{
    static class NullExceptionGuard
    {
        public static TProp SafelyExp<TContainer, TProp>(this TContainer me, Expression<Func<TContainer, TProp>> selector, TProp def)
        {
            var propVal = def;

            if (me == null) return propVal;

            var selectorParts = new Stack<Expression>();

            var body = selector.Body as MemberExpression;
            if (body == null) throw new InvalidOperationException("Only selector expressions are allowed. Ex.: x => x.Name.Length");

            selectorParts.Push(body);

            var memberAccess = body.Expression as MemberExpression;
            while (memberAccess != null)
            {
                selectorParts.Push(memberAccess);
                memberAccess = memberAccess.Expression as MemberExpression;
            }

            // TODO: What if x => x

            var firstParam = ((MemberExpression)selectorParts.Peek()).Expression as ParameterExpression;
            if (firstParam == null) throw new InvalidOperationException("Only selector expressions are allowed. Ex.: x => x.Name.Length");
            selectorParts.Push(firstParam);

            var nullCheckParts = new Queue<Expression>();
            while (selectorParts.Count > 1)
            {
                var selPart = selectorParts.Pop();
                var nullCheckExp = Expression.NotEqual(selPart, Expression.Constant(null));
                nullCheckParts.Enqueue(nullCheckExp);
            }

            Expression chainedAndExp = null;
            while (nullCheckParts.Count > 0)
            {
                var nullCheckPart = nullCheckParts.Dequeue();

                if (chainedAndExp == null)
                {
                    chainedAndExp = Expression.AndAlso(Expression.Constant(true), nullCheckPart);
                }
                else
                {
                    chainedAndExp = Expression.AndAlso(chainedAndExp, nullCheckPart);
                }
            }

            var safeExp = Expression.Condition(chainedAndExp, selectorParts.Pop(), Expression.Constant(def));
            var safeLam = Expression.Lambda(safeExp, selector.Parameters);

            var safeFun = (Func<TContainer, TProp>)safeLam.Compile();
            propVal = safeFun(me);

            return propVal;
        }
    }
}
