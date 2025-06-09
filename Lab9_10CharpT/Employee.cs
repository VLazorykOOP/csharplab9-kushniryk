using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR9
{
    class Employee
    {
        public string LastName;
        public string FirstName;
        public string MiddleName;
        public string Gender;
        public int Age;
        public decimal Salary;

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName} {Gender} {Age} {Salary}";
        }
    }

    class Employee2 : ICloneable, IComparable
    {
        public string LastName;
        public string FirstName;
        public string MiddleName;
        public string Gender;
        public int Age;
        public decimal Salary;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public int CompareTo(object obj)
        {
            if (obj is Employee2 other)
                return this.Age.CompareTo(other.Age);
            throw new ArgumentException("Object is not an Employee2");
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName} {Gender} {Age} {Salary}";
        }
    }
}
