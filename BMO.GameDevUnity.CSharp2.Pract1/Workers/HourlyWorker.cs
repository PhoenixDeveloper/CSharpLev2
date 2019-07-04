using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class HourlyWorker : Worker
    {
        public HourlyWorker(string lastName, string firstName, double salary)
        {
            LastName = lastName;
            FirstName = firstName;
            Salary = salary;
        }

        public override double WagesPerMonth()
        {
            return Math.Round((20.8 * 8 * Salary), 2);
        }
    }
}
