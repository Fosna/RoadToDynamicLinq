using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpTreeShowoff
{
    partial class Program
    {
        private static IEnumerable<T> FilterBy<T>(IEnumerable<T> collection, string filterPropName, string filterValue)
        {
            // Ex.: x => x.Age == 3
            Func<T, bool> constraint = GetFilterConstraint<T>(filterPropName, filterValue);

            var filteredCollection = collection.Where(constraint);
            return filteredCollection;
        }

        private static Func<T, bool> GetFilterConstraint<T>(string filterPropName, string filterValue)
        {
            // Ex.: x => x.Age == 3

            // x on the left side.
            ParameterExpression param = Expression.Parameter(typeof(T), "x");
            // x.Age
            MemberExpression getPropValue = Expression.Property(param, filterPropName);

            // 3
            ConstantExpression compareToConst = GetConstant(filterValue, getPropValue);

            // x.Age == 3
            BinaryExpression comparison = Expression.Equal(getPropValue, compareToConst);
            // x => x.Age == 3
            LambdaExpression compareExp = Expression.Lambda(comparison, param);

            // Some MSIL
            Func<T, bool> constraint = (Func<T, bool>)compareExp.Compile();
            return constraint;
        }

        private static ConstantExpression GetConstant(string filterValue, MemberExpression getPropValue)
        {
            ConstantExpression compareToConst;
            if (getPropValue.Type == typeof(int))
            {
                var packedFilterValue = int.Parse(filterValue);
                compareToConst = Expression.Constant(packedFilterValue);
            }
            else if (getPropValue.Type == typeof(string))
            {
                compareToConst = Expression.Constant(filterValue);
            }
            else
            {
                throw new NotSupportedException("Descriptive message for demo!");
            }
            return compareToConst;
        }
    }
}
