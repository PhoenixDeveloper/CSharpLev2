using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountElementsInList
{
    class Citizen:IComparable<Citizen>
    {
        public string Name { get; set; }

        public int Age { get; set; }

        static Random random = new Random();

        public Citizen(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public Citizen()
        {
            Name = $"Ива{random.Next(9)}ов{random.Next(100)}";
            Age = random.Next(10, 100);
        }

        public int CompareTo(Citizen other)
        {
            if (Age > other.Age) return 1;
            if (Age < other.Age) return -1;
            return 0;           
        }
    }
}
