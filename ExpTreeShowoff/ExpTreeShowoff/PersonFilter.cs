using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpTreeShowoff
{
    partial class Program
    {
        private static IEnumerable<Person> CustomFilterBy(IEnumerable<Person> crew, string filterPropName, string filterValue)
        {
            // TODO: Argument validation.

            // Ex.: x => x.Age == 3
            Func<Person, bool> filterConstraint = GetCustomFilterConstraint(filterPropName, filterValue);

            var filteredCrew = crew.Where(filterConstraint);
            return filteredCrew;
        }

        private static Func<Person, bool> GetCustomFilterConstraint(string filterPropName, string filterValue)
        {
            Func<Person, bool> filterConstraint = null;
            if (filterPropName == "Name")
            {
                filterConstraint = x => x.Name == filterValue;
            }
            else if (filterPropName == "Age")
            {
                var packedValue = int.Parse(filterValue);
                filterConstraint = x => x.Age == packedValue;
            }
            else
            {
                filterConstraint = x => true;
            }
            return filterConstraint;
        }
    }
}
