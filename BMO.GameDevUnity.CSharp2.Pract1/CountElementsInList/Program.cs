using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountElementsInList
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> listInteger = new List<int>()
            {
                5,
                6,
                2,
                5,
                3,
                2
            };
            foreach (var element in countElementInListInteger(listInteger))
            {
                Console.WriteLine($"Число {element.Key} встречается в списке {element.Value} раз");
            }
            Console.ReadKey();
        }

        static Dictionary<int, int> countElementInListInteger(List<int> list)
        {
            list.Sort();
            Dictionary<int, int> count = new Dictionary<int, int>();
            foreach (var element in list)
            {
                if (!count.ContainsKey(element))
                {
                    count.Add(element, 0);
                }
                count[element]++;
            }
            return count;
        }
    }
}
