using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine
{
    static class AdminLogic
    {
        static string adminName;

        public static bool IsAdmin(string input)
        {
            EmployeesDatabase employees = new EmployeesDatabase();
            foreach (var emp in employees.Employees)
            {
                if(emp.Password == input)
                {
                    adminName = emp.Name;
                    return true;
                }
            }
            return false;
        }

        public static void StartAdminLogic()
        {
            int input = CheckAdminInfo();
            ChooseAction(input);
            Console.ReadKey();
        }

        private static void ChooseAction(int input)
        {
            switch(input)
            {
                case 1:
                    AddProduct();
                    break;
                case 2:
                    ShowTransactions();
                    break;
                default:
                    Console.WriteLine("Coś poszło nie tak!");
                    break;
            }
        }

        private static void ShowTransactions()
        {
            ProductsDatabase productsDatabase = new ProductsDatabase();
            Console.WriteLine(productsDatabase.ShowAdmin());
        }

        private static void AddProduct()
        {
            int input = -1;
            bool correctInput = false;
            Console.Clear();
            ShowTransactions();
            do
            {
                Console.WriteLine("Wybierz prododukt który chcesz dodać "+adminName);
                try
                {
                    input = int.Parse(Console.ReadLine());
                    correctInput = true;
                }
                catch (System.FormatException)
                {
                    correctInput = false;
                }
            } while (!correctInput);            


        }

        private static int CheckAdminInfo()
        {
            bool correctInput = false;
            int input = -1;
            do
            {
                Console.WriteLine("Siema " + adminName + " czy chcesz: \n" + "1 - Dodac produkt \n2 - Zobaczyc liste transakcji");
                try
                {
                    input = int.Parse(Console.ReadLine());
                    correctInput = true;
                }
                catch (System.FormatException)
                {
                    correctInput = false;
                }
            } while (!correctInput);
            return input;
        }
    }
}
