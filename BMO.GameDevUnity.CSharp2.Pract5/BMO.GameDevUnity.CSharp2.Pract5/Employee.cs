using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMO.GameDevUnity.CSharp2.Pract5
{
    public class Employee
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Profession { get; set; }
        public int Age { get; set; }

        public Employee(string lastName, string firstName, string profession, int age)
        {
            LastName = lastName;
            FirstName = firstName;
            Profession = profession;
            Age = age;
        }

        public Employee()
        {

        }

        public override string ToString()
        {
            return LastName + " " + FirstName + " " + Profession + " " + Age;
        }
    }
}
