using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Model
{
    class EmployeesDatabase
    {
        private List<Employee> employees;

        public List<Employee> Employees { get => employees; }

        public EmployeesDatabase()
        {
            employees = new List<Employee>();
            //read from databse
        }
    }
}
