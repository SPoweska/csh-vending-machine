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
            TransactionsDatabase transactionsDatabase = new TransactionsDatabase();
            Console.WriteLine(transactionsDatabase.ShowTransactions());
        }

        private static void AddNewProduct() //Dokończyć i uporządkować
        {
            bool correctInputP=false;
            bool correctInputQ=false;
            bool correctInput = false;
            string name;
            float price=0f;
            int quantity=-1;
            int input=-1;
            
            Console.Clear();
            Console.WriteLine("Dodaj nowy produkt \n" + "Podaj nazwę produktu:");
            name = Console.ReadLine();
            do
            {
                Console.WriteLine("Podaj cenę :");
                try

                {
                    price = float.Parse(Console.ReadLine());
                    correctInputP = true;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Zrobiłeś coś źle, podaj wyprować daną jeszcze raz");
                    correctInputP = false;
                }
            } while (!correctInputP);

            do
            { Console.WriteLine("Podaj ilość :");
                try
                {
                    quantity = int.Parse(Console.ReadLine());
                    if (quantity >= 1)
                    {
                        correctInputQ = true;
                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Zrobiłeś coś źle, podaj wyprować daną jeszcze raz");
                    correctInputQ = false;
                }
            } while (!correctInputQ);  
            

            do
            {
                Console.WriteLine("Czy wszystko się zgadza " + adminName + "?\n" + "Nazwa: " + name + " cena: " + price + " ilość: " + quantity);
                Console.WriteLine("1 - Wszystko git  \n 2 - Chce poprawić dane \n");
                try
                {
                    input = int.Parse(Console.ReadLine());
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Wybierz dostępną opcję");
                }
            } while (!correctInput);


            switch (input)
            {
                case 1:
                    ProductsDatabase.AddNewProduct(name, price, quantity);
                    break;
                case 2:
                    AddNewProduct();
                    break;               
                default:
                    Console.WriteLine("Nie ma takiej opcji");
                    break;
            }

        }

        private static void AddProduct() //Dokończyć
        {
            bool choosedProduct = false;
            int input = -1;
            int choice = 0;
            int prod = 0;
            int increment = 1;
            bool correctInput = false;
            Console.Clear();
            ProductsDatabase products = new ProductsDatabase();
            Console.WriteLine(products.ShowAdmin());


            do
            {
                Console.WriteLine("Wybierz produkt który chcesz uzupełnić " + adminName);
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
            Console.WriteLine("Czy chcesz dodać produkt numer " + input + "?\n" + "1 - Tak\n" + "2 - Chce dodać inny produkt" + "3 - Nie");
            do
            {
                try
                {
                    choice = int.Parse(Console.ReadLine());
                    correctInput = true;
                }
                catch (System.FormatException)
                {
                    correctInput = false;
                }
            } while (!correctInput);
            //switch (choice)
            //{
            //    case 1:
            //        int length = products.Products.Count;
            //        for (int i = 1; i <= length; i++)
            //        {
            //            if (i == prod)
            //            {
            //                //increment choosed product quantity
            //                choosedProduct = true;
            //                using (SQLiteConnection conn = new SQLiteConnection("Data Source=VMbaza.db;Version=3;New=False;Compress=True;"))
            //                {
            //                    conn.Open();
            //                    SQLiteCommand cmd = conn.CreateCommand();
            //                    cmd.CommandText = "UPDATE Products SET Quantity = Quantity - '" + increment + "' WHERE ID='" + prod + "'";
            //                    cmd.ExecuteNonQuery();

            //                }
            //            }
            //        }


            //    default:
            //        break;
            //}
        }
            
            private static int CheckAdminInfo()
        {
            bool correctInput = false;
            int input = -1;
            do
            {
                Console.WriteLine("Siema " + adminName + " czy chcesz: \n" + "1 - Uzupełnić produkt \n" + "2 - Zobaczyc liste transakcji \n" + "3 - Dodać nowy produkt");
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
