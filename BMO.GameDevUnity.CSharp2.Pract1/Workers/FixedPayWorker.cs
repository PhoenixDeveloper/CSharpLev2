using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class FixedPayWorker : Worker
    {
        public override double WagesPerMonth()
        {
            return Math.Round(Salary, 2);
        }
    }
}
