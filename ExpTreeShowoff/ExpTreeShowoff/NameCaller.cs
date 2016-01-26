using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpTreeShowoff
{
    static class NameCaller
    {
        public static string GetPropertyName<T>(Expression<Func<T, object>> memberSelector)
        {
            var lamExp = memberSelector as LambdaExpression;
            ThrowIfNull(lamExp);

            var bodyCast = lamExp.Body as UnaryExpression;
            ThrowIfNull(bodyCast);

            var member = bodyCast.Operand as MemberExpression;
            ThrowIfNull(member);

            var propName = member.Member.Name;
            return propName;
        }

        private static void ThrowIfNull(Expression exp)
        {
            if (exp == null)
            {
                throw new NotSupportedException("Descriptive message for demo!");
            }
        }
    }
}
