using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpTreeShowoff
{
    partial class Program
    {
        private static IEnumerable<Person> CustomSortBy(IEnumerable<Person> crew, string sortPropName, string sortOrder)
        {
            // TODO: Argument validation

            // Ex.: x => x.Age
            Func<Person, object> sortSelector = GetCustomSortSelector(sortPropName);

            var sorted = SortAscOrDesc(crew, sortSelector, sortOrder);
            return sorted;
        }

        private static Func<Person, object> GetCustomSortSelector(string sortPropName)
        {
            Func<Person, object> sortSelector = null;
            if (sortPropName == "Name")
            {
                sortSelector = x => x.Name;
            }
            else if (sortPropName == "Age")
            {
                sortSelector = x => x.Age;
            }
            else
            {
                sortSelector = x => true;
            }
            return sortSelector;
        }
    }
}
