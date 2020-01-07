using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeesDatabase emp = new EmployeesDatabase();
            Console.WriteLine(emp.FetchEmployeeById(1).Name);
            ProductsDatabase prod = new ProductsDatabase();
            Console.WriteLine(prod.FetchProductById(2).Name);
            Console.ReadKey();
        }

        /// <summary>
        /// Shows given text to the console
        /// </summary>
        /// <param name="text">text to show</param>
        private void Output(string text)
        {
            Console.WriteLine(text);
        }
    }
}
