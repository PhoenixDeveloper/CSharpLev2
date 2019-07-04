using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class FixedPayWorker : Worker
    {
        public FixedPayWorker(string lastName, string firstName, double salary)
        {
            LastName = lastName;
            FirstName = firstName;
            Salary = salary;
        }

        public override double WagesPerMonth()
        {
            return Math.Round(Salary, 2);
        }
    }
}
