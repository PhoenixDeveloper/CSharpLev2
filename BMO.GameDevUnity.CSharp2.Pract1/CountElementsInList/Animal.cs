using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountElementsInList
{
    class Animal:IComparable<Animal>
    {
        /// <summary>
        /// Вид животного
        /// </summary>
        public string Kind { get; set; }

        public bool Rare { get; set; }

        public int Worth { get; set; }

        static int count = 1;

        static Random random = new Random();

        public Animal(string kind, bool rare, int worth)
        {
            Kind = kind;
            Rare = rare;
            Worth = worth;
        }

        public Animal()
        {
            Kind = $"Вид №{count}";
            count++;
            if (count % 2 == 0)
            {
                Rare = true;
            }
            else
            {
                Rare = false;
            }
            if (Rare)
            {
                Worth = random.Next(1, 10) * 50;
            }
            else
            {
                Worth = random.Next(75);
            }
        }

        public int CompareTo(Animal other)
        {
            if (Worth > other.Worth) return 1;
            if (Worth < other.Worth) return -1;
            return 0;
        }
    }
}
