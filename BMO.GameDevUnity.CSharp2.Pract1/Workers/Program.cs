using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker[] workers = new Worker[10];
            Random random = new Random();
            for (int i = 0; i < workers.Length; i++)
            {
                if (i%2==0)
                {
                    workers[i] = new FixedPayWorker($"Тес{i}ов{random.Next(i, i * i)}", $"Мих{random.Next(i, i * i)}ил{i}", (random.NextDouble() * 75000) + 15000);
                }
                else
                {
                    workers[i] = new HourlyWorker($"Сми{i}ов{random.Next(i, i * i)}", $"Ви{random.Next(i, i * i)}тор{i}", (random.NextDouble() * 350) + 200);
                }
            }
            Array.Sort(workers);
            ArrayWorkers arrayWorkers = new ArrayWorkers(workers);
            foreach (var worker in arrayWorkers)
            {
                Console.WriteLine($"Сотрудник: {worker.LastName} {worker.FirstName}. Среднемесячная заработная плата: {worker.WagesPerMonth}");
            }
            Console.ReadKey();
        }
    }
}
