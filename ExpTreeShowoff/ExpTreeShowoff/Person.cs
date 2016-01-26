using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpTreeShowoff
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Person Child { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0}, Age: {1}", Name, Age);
        }
    }
}
