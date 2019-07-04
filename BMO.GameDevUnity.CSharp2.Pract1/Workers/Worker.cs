using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    abstract class Worker:IComparable<Worker>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        private double salary;
        public double Salary
        {
            get
            {
                return Math.Round(salary, 2);
            }
            set
            {
                if (value < 0)
                {
                    salary = -Math.Round(value, 2);
                }
                else
                {
                    salary = Math.Round(value, 2);
                }
            }
        }

        abstract public double WagesPerMonth();

        public int CompareTo(Worker other)
        {
            if (WagesPerMonth() > other.WagesPerMonth()) return 1;
            if (WagesPerMonth() < other.WagesPerMonth()) return -1;
            return 0;
        }
    }
}
