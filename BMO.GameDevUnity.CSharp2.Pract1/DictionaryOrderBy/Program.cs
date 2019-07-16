using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryOrderBy
{
    class Program
    {
        delegate int OrderDeletate(KeyValuePair<string, int> pair);
        static void Main(string[] args)
        {
            OrderDeletate orderDeletate = new OrderDeletate(KeyValuePairMethod);
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                { "four",4 },
                { "two",2 },
                { "one",1 },
                { "three",3 },
            };
            var d = dict.OrderBy(orderDeletate.Invoke);
            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
            Console.ReadKey();
        }

        static int KeyValuePairMethod(KeyValuePair<string, int> pair)
        {
            return pair.Value;
        }
    }
}
