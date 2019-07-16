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
            Citizen citizenBuffer = new Citizen("Иванов Иван", 20);
            List<Citizen> citizens = new List<Citizen>()
            {
                citizenBuffer,
                new Citizen("Чепчиков Владимир", 34),
                citizenBuffer
            };
            Animal animalBuffer = new Animal("Человек разумный", false, 1000);
            List<Animal> animals = new List<Animal>()
            {
                animalBuffer,
                animalBuffer,
                animalBuffer
            };
            for (int i = 0; i < 10; i++)
            {
                citizens.Add(new Citizen());
                animals.Add(new Animal());
            }
            Console.WriteLine("Список Integer:");
            foreach (var element in countElementInListInteger(listInteger))
            {
                Console.WriteLine($"Число {element.Key} встречается в списке {element.Value} раз");
            }
            Console.WriteLine("Список Citizen");
            foreach (var citizen in countElementInGenericList(citizens))
            {
                Console.WriteLine($"Гражданин с именем {citizen.Key.Name}, {citizen.Key.Age} лет встречается в списке {citizen.Value} раз");
            }
            Console.WriteLine("Список Animal");
            foreach (var animal in countElementInGenericList(animals))
            {
                if (animal.Key.Rare)
                {
                    Console.WriteLine($"Животное редкого вида {animal.Key.Kind} ценностью {animal.Key.Worth} встречается в списке {animal.Value} раз");
                }
                else
                {
                    Console.WriteLine($"Животное вида {animal.Key.Kind} ценностью {animal.Key.Worth} встречается в списке {animal.Value} раз");
                }
            }
            Dictionary<Animal, int> countAnimals = new Dictionary<Animal, int>();
            List<Animal> animalsBuffer = animals.ToList();
            while (!(animalsBuffer.Count == 0))
            {
                animalBuffer = animalsBuffer[0];
                countAnimals.Add(animalBuffer, animalsBuffer.FindAll(i => i == animalBuffer).Count);
                animalsBuffer.RemoveAll(i => i == animalBuffer);
            }
            Console.WriteLine("Список Animal при помощи Linq-запросов");
            foreach (var animal in countAnimals)
            {
                if (animal.Key.Rare)
                {
                    Console.WriteLine($"Животное редкого вида {animal.Key.Kind} ценностью {animal.Key.Worth} встречается в списке {animal.Value} раз");
                }
                else
                {
                    Console.WriteLine($"Животное вида {animal.Key.Kind} ценностью {animal.Key.Worth} встречается в списке {animal.Value} раз");
                }
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

        static Dictionary<T, int> countElementInGenericList<T>(List<T> list)
        {
            list.Sort();
            Dictionary<T, int> count = new Dictionary<T, int>();
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
