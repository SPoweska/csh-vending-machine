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
        private int id;
        private string name;
        private string password;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
    }
}
