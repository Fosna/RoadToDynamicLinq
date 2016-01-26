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
        static void Main(string[] args)
        {
            var crew = new List<Person> 
            { 
                new Person { Name = "Phill", Age = 43 },
                new Person { Name = "Claire", Age = 45 },
                new Person { Name = "Jay", Age = 65 },
            };

            ShowOffCustomSort(crew, "Name", "asc");
            ShowOffSort(crew, "Name", "asc");

            ShowOffCustomSort(crew, "Age", "desc");
            ShowOffSort(crew, "Age", "desc");


            ShowOffCustomFilter(crew, "Age", "43");
            ShowOffFilter(crew, "Age", "43");

            ShowOffCustomFilter(crew, "Name", "Jay");
            ShowOffFilter(crew, "Name", "Jay");


            ShowOffGettingPropertyName();

            var phill = crew.First();
            ShowOffChildNameRecklessly(phill);

            ShowOffChildNameSafely(phill);
            
            phill.Child = new Person 
            { 
                Age = 21,
                Name = "Alex",
            };
            ShowOffChildNameSafely(phill);
        }

        private static void ShowOffChildNameSafely(Person phill)
        {
            var childName = phill.SafelyExp(x => x.Child.Name, "n/a");
            Console.WriteLine();
            Console.WriteLine("Getting Phill's child name safely.");
            Console.WriteLine("Phill's child name: {0}", childName);
        }

        private static void ShowOffChildNameRecklessly(Person phill)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Getting Phill's child name recklessly.");
                var compilerWantsMe = phill.Child.Name;
            }
            catch (NullReferenceException nullEx)
            {
                Console.WriteLine("Exception here!!! {0}", nullEx.Message);
            }
        }

        private static void ShowOffGettingPropertyName()
        {
            var nameOfAge = NameCaller.GetPropertyName<Person>(x => x.Age);
            Console.WriteLine("Name of age property is: '{0}'.", nameOfAge);
        }


        private static void ShowOffFilter(List<Person> crew, string filterPropName, string filterValue)
        {
            var filteredCrew = FilterBy(crew, filterPropName, filterValue);

            Console.WriteLine("Filter by {0} = {1}", filterPropName, filterValue);
            filteredCrew.ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine();
        }


        private static void ShowOffCustomFilter(IEnumerable<Person> crew, string filterPropName, string filterValue)
        {
            var filteredCrew = CustomFilterBy(crew, filterPropName, filterValue);

            var defColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Custom ");
            Console.ForegroundColor = defColor;

            Console.WriteLine("filter by {0} = {1}", filterPropName, filterValue);
            filteredCrew.ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine();
        }


        private static void ShowOffCustomSort(IEnumerable<Person> crew, string sortPropName, string sortOrder)
        {
            var sortedCrew = CustomSortBy(crew, sortPropName, sortOrder);

            var defColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Custom ");
            Console.ForegroundColor = defColor;

            Console.WriteLine("sort by {0} {1}", sortPropName, sortOrder);
            sortedCrew.ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine();
        }


        private static void ShowOffSort(IEnumerable<Person> crew, string sortPropName, string sortOrder)
        {
            var sortedCrew = SortBy(crew, sortPropName, sortOrder);
            Console.WriteLine("Sort by {0} {1}", sortPropName, sortOrder);
            sortedCrew.ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine();
        }
    }
}
