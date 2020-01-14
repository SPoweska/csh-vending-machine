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
                case 3:
                    AddNewProduct();
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

        private static void AddNewProduct() //Dokończyć i uporządkować
        {            
            string name;
            float price=0f;
            int quantity=0;
            int input;
            
            Console.Clear();
            Console.WriteLine("Dodaj nowy produkt \n" + "Podaj nazwę produktu:");
            name = Console.ReadLine();
            Console.WriteLine("Podaj cenę :");
                try
                
                { 
                    price = float.Parse(Console.ReadLine());                   
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Coś nie pykło");

                }
            Console.WriteLine("Podaj ilość :");
                try
                {
                    quantity = int.Parse(Console.ReadLine());
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Coś nie pykło");
                }
            Console.WriteLine("Czy wszystko się zgadza " + adminName + "?\n" + "Nazwa: " + name + " cena: " + price + " ilość: " + quantity);
            Console.WriteLine("1 - Wszystko git  \n 2 - Chce poprawić dane \n");
                try
                {
                    input = int.Parse(Console.ReadLine());
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Coś nie pykło");
                }
            input =int.Parse(Console.ReadLine());
            switch (input)
            {
                case 1:
                    ProductsDatabase.AddNewProduct(name, price, quantity);
                    break;
                case 2:
                    AddNewProduct();
                    break;                
                default:
                    Console.WriteLine("Coś poszło nie tak!");
                    break;
            }

        }
        private static void AddProduct() //Dokończyć
        {
            int input = -1;
            bool correctInput = false;
            Console.Clear();
            ShowTransactions();
            do
            {
                Console.WriteLine("Wybierz produkt który chcesz uzupełnić "+adminName);
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
                Console.WriteLine("Siema " + adminName + " czy chcesz: \n" + "1 - Uzupełnić produkt \n 2 - Zobaczyc liste transakcji \n"+"3 - Dodać nowy produkt");
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
