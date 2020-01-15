using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Model
{
    /// <summary>
    /// Model of an employee
    /// </summary>
    class Employee
    {
        private Int64 id;
        private string name;
        private string password;

        public Int64 Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }

        public Employee(Int64 id, string name, string password)
        {
            this.id = id;
            this.name = name;
            this.password = password;
        }
    }
}
