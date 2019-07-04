using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class ArrayWorkers:IEnumerable<Worker>, IEnumerator<Worker>
    {
        int lenght;
        int indexNew = -1;
        Worker[] workers;
        Random random = new Random();

        public int Lenght
        {
            get
            {
                return lenght;
            }
            set
            {
                if (value < 0)
                {
                    lenght = -value;
                }
                else
                {
                    lenght = value;
                }
            }
        }

        public Worker Current => workers[this.indexNew];

        object IEnumerator.Current => workers[this.indexNew];

        public ArrayWorkers(int lenght)
        {
            Lenght = lenght;
            workers = new Worker[Lenght];

            for (int i = 0; i < workers.Length; i++)
            {
                if (i % 2 == 0)
                {
                    workers[i] = new FixedPayWorker($"Тес{i}ов{random.Next(i, i * i)}", $"Мих{random.Next(i, i * i)}ил{i}", (random.NextDouble() * 75000) + 15000);
                }
                else
                {
                    workers[i] = new HourlyWorker($"Сми{i}ов{random.Next(i, i * i)}", $"Ви{random.Next(i, i * i)}тор{i}", (random.NextDouble() * 350) + 200);
                }
            }
        }

        public ArrayWorkers(Worker[] sourceArray)
        {
            Lenght = sourceArray.Length;
            workers = new Worker[Lenght];

            for (int i = 0; i < sourceArray.Length; i++)
            {
                if (sourceArray[i] is FixedPayWorker)
                {
                    this[i] = new FixedPayWorker(sourceArray[i].LastName, sourceArray[i].FirstName, sourceArray[i].Salary);
                }
                else if (sourceArray[i] is HourlyWorker)
                {
                    this[i] = new HourlyWorker(sourceArray[i].LastName, sourceArray[i].FirstName, sourceArray[i].Salary);
                }
            }
        }

        public Worker this[int index]
        {
            get
            {
                return workers[index];
            }
            set
            {
                workers[index] = value;
            }
        }

        public IEnumerator<Worker> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (this.indexNew == this.workers.Length - 1)
            {
                Reset();
                return false;
            }
            this.indexNew++;
            return true;
        }

        public void Reset()
        {
            this.indexNew = -1;
        }

        public void Dispose()
        {
        }
    }
}
