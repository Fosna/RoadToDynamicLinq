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
        private static IEnumerable<T> SortBy<T>(IEnumerable<T> collection, string sortPropName, string sortOrder)
        {
            // TODO: Argument validation

            // Ex.: x => x.Age
            Func<T, object> sortSelector = GetSortSelector<T>(sortPropName);
            
            var sorted = SortAscOrDesc(collection, sortSelector, sortOrder);
            return sorted;
        }

        private static Func<T, object> GetSortSelector<T>(string sortPropName)
        {
            // Ex.: x => x.Age

            // x on the left side.
            ParameterExpression param = Expression.Parameter(typeof(T), "x");
            // x.Age
            MemberExpression prop = Expression.Property(param, sortPropName);

            UnaryExpression propAsObj = Expression.Convert(prop, typeof(object));

            // x => x.Age
            LambdaExpression sortSelectorExp = Expression.Lambda(propAsObj, param);

            // Some MSIL
            var sortSelector = (Func<T, object>)sortSelectorExp.Compile();

            return sortSelector;
        }

        private static IEnumerable<T> SortAscOrDesc<T>(IEnumerable<T> collection, Func<T, object> sortSelector, string sortOrder)
        {
            var sorted = Enumerable.Empty<T>();

            if (sortOrder.ToLower() == "asc")
            {
                sorted = collection.OrderBy(sortSelector);
            }
            else if (sortOrder.ToLower() == "desc")
            {
                sorted = collection.OrderByDescending(sortSelector);
            }
            return sorted;
        }
    }
}
