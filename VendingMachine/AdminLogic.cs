using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Model;
using System.Threading;

namespace VendingMachine
{ 
/// <summary>
/// All the admin logic
/// </summary>
class AdminLogic
    {
        static string adminName;
        /// <summary>
        /// Checks input looking for password
        /// <param name="input">Admin action selector</param>
        /// </summary>
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
        /// <summary>
        /// Starts admin logic        
        /// </summary>
        public static void StartAdminLogic()
        {
            Console.Clear();
            int input = CheckAdminInfo();
            ChooseAction(input);
            Console.ReadKey();
        }
        /// <summary>
        /// Main homescreen        
        /// </summary>
        public static int CheckAdminInfo()
        {
            Console.Clear();
            bool correctInput = false;
            int input = -1;
            do
            {
                Console.WriteLine("Siema, " + adminName + " czy chcesz: \n" + "1 - Uzupełnić produkt \n" + "2 - Zobaczyc liste transakcji \n" + "3 - Dodać nowy produkt\n"+"4 - Zmniejszyć ilość produktu\n"+"5 - Usunąć produkt\n" + "9 - Opuścić tryb administratora");
                try
                {
                    input = int.Parse(Console.ReadLine());
                    if (input > 0)
                    {
                        correctInput = true;
                    }
                    else { correctInput = false; }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("To nie jest jedna z opcji");
                    correctInput = false;
                }
            } while (!correctInput);
            return input;
        }
        /// <summary>
        /// Main admin menu
        /// <param name="input">Admin action selector</param>
        /// </summary>
        private static void ChooseAction(int input)
        {            
            if (input > 0) {
                switch (input)
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
                    case 4:
                        DropProduct();
                        break;
                    case 5:
                        DeleteProduct();
                        break;
                    case 9:
                        MachineLogic.StartMachine();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Coś poszło nie tak!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        CheckAdminInfo();
                        break;
                }                
            }
            else {
                Console.Clear();
                Console.WriteLine("Błąd - spóbuj ponownie");
                Thread.Sleep(2500);
                CheckAdminInfo();
            }
        }
        /// <summary>
        /// Transaction menu selection
        /// </summary>
        public static void ChooseTransAction()
        {
            Console.Clear();
            int input = -1;
           bool correctInput = false;
            Console.WriteLine("\n" + "Czy chcesz:\n" + "1 - Wyeksportować transakcje do pliku CSV\n" + "2 - Wyczyścić listę transakcji\n" + "9 - Wrócić do poprzedniego ekranu");            
            do
            {
                try
                {
                    input = int.Parse(Console.ReadLine());
                    if (input > 0)
                    {
                        correctInput = true;
                    }
                    else { correctInput = false; }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("To nie jest jedna z opcji");
                    Thread.Sleep(3000);
                    Console.Clear();
                    correctInput = false;
                }
            } while (!correctInput);
            switch (input)
            {
                case 1:
                    TransactionsDatabase.ExportCSV();                    
                    break;
                case 2:
                    TransactionsDatabase.CleanTransactions();                    
                    break;
                case 9:
                    StartAdminLogic();
                    break;
                default:
                    Console.WriteLine("Coś poszło nie tak");
                    Thread.Sleep(3000);                    
                    ChooseTransAction();
                    break;
            }
        }
        /// <summary>
        /// Updates product quantity in database
        /// </summary>
        public static void AddProduct()
        {
            int input = -1;
            bool correctInput = false;
            int quantity = -1;
            Console.Clear();
            ProductsDatabase products = new ProductsDatabase();
            Console.WriteLine(products.ShowAdmin());


            do
            {
                Console.WriteLine("Wybierz produkt który chcesz uzupełnić " + adminName);
                try
                {
                    input = int.Parse(Console.ReadLine());
                    if (input > 0)
                    {
                        correctInput = true;
                    }
                    else { correctInput = false; }

                }
                catch (System.FormatException)
                {
                    correctInput = false;
                    Thread.Sleep(3000);
                    Console.Clear();
                }
            } while (!correctInput);
            do
            {
                Console.WriteLine("Ile sztuk chcesz dodać?  " + adminName);
                try
                {
                    quantity = int.Parse(Console.ReadLine());
                    if (quantity > 0)
                    {
                        correctInput = true;
                    }
                    else { correctInput = false; }

                }
                catch (System.FormatException)
                {
                    correctInput = false;
                }
            } while (!correctInput);

            Console.WriteLine(adminName + " Czy chcesz dodać " + quantity + " sztuk produktu numer " + input + "?\n" + "1 - Tak\n" + "2 - Nie\n" + "3 - Chce dodać inny produkt");
            int actionChoice = int.Parse(Console.ReadLine());

            switch (actionChoice)
            {
                case 1:
                    ProductsDatabase.Increment(input, quantity, products);
                    Console.WriteLine("Dodano");
                    Thread.Sleep(2500);
                    StartAdminLogic();
                    break;
                case 2:
                    StartAdminLogic();
                    break;
                case 3:
                    AddProduct();
                    break;
                default:
                    Console.WriteLine("Coś poszło nie tak!");
                    Thread.Sleep(2500);
                    StartAdminLogic();
                    break;
            }
        }
        /// <summary>
        /// Shows transaction
        /// </summary>
        public static void ShowTransactions()
        {            
            TransactionsDatabase transactionsDatabase = new TransactionsDatabase();
            Console.WriteLine(transactionsDatabase.ShowTransactions());
            ChooseTransAction();
        }
        /// <summary>
        /// Adds new product to database
        /// </summary>
        public static void AddNewProduct()
        {            
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
                    if (price > 0)
                    {
                        correctInput = true;
                    }
                    else { correctInput = false; }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Zrobiłeś coś źle, wyprować daną jeszcze raz");
                    correctInput = false;
                }
            } while (!correctInput);

            do
            { Console.WriteLine("Podaj ilość :");
                try
                {
                    quantity = int.Parse(Console.ReadLine());
                    if (quantity > 0)
                    {
                        correctInput = true;
                    }
                    else { correctInput = false; }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Zrobiłeś coś źle, wyprować daną jeszcze raz");
                    correctInput = false;
                }
            } while (!correctInput);  
            

            do
            {
                Console.WriteLine("Czy wszystko się zgadza " + adminName + "?\n" + "Nazwa: " + name + " cena: " + price + " ilość: " + quantity);
                Console.WriteLine("1 - Wszystko git  \n 2 - Chce poprawić dane \n"+"3 - Anuluj i wróć do menu administratora ");
                try
                {
                    input = int.Parse(Console.ReadLine());
                    if (input > 0)
                    {
                        correctInput = true;
                    }
                    else { correctInput = false; }
                }
                catch (System.FormatException)
                {
                    correctInput = false;
                    Console.WriteLine("To nie jest jedna z opcji");
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
                case 3:
                    StartAdminLogic();
                    break;
                default:
                    Console.WriteLine("To nie jest jedna z opcji");
                    break;
            }

        }
        /// <summary>
        /// Drops product quantity
        /// </summary>
        public static void DropProduct()
        {
            int input = -1;
            bool correctInput = false;
            int quantity = -1;
            int actionChoice = -1;
            int prod = 0;
            Console.Clear();
            ProductsDatabase products = new ProductsDatabase();
            Console.WriteLine(products.ShowAdmin());
            Int64 productQuantity;


            do
            {
                Console.WriteLine("Wybierz produkt którego ilość chcesz zmniejszyć " + adminName);
                try
                {
                    input = int.Parse(Console.ReadLine());
                    if (input > 0)
                    {
                        correctInput = true;
                    }
                    else { correctInput = false; }

                }
                catch (System.FormatException)
                {
                    correctInput = false;
                }
            } while (!correctInput);
            do
            {
                Console.WriteLine("Ile sztuk chcesz wyjąć?  " + adminName);
                try
                {
                    quantity = int.Parse(Console.ReadLine());
                    if (quantity > 0)
                    {
                        correctInput = true;
                    }
                    else { correctInput = false; }

                }
                catch (System.FormatException)
                {
                    correctInput = false;
                }
            } while (!correctInput);
            productQuantity = Convert.ToInt32(products.Products[prod - 1].Quantity);
            
            if (quantity>=productQuantity)
            {
                quantity = Convert.ToInt32(productQuantity);
            }
            Console.WriteLine(adminName + " Czy chcesz wyjąć " + quantity + " sztuk produktu numer " + input + "?\n" + "1 - Tak\n" + "2 - Nie\n" + "3 - Chce wyjąć inny produkt");
            do
            {
                try
                {
                    actionChoice = int.Parse(Console.ReadLine());
                    if (actionChoice > 0)
                    {
                        correctInput = true;
                    }
                    else { correctInput = false; }
                }
                catch (System.FormatException)
                {
                    correctInput = false;
                }
            } while (!correctInput);

            switch (actionChoice)
            {
                case 1:
                    ProductsDatabase.DropQuantity(input, quantity, products);
                    Console.WriteLine("Wyjęto");
                    Thread.Sleep(2500);
                    StartAdminLogic();
                    break;
                case 2:
                    StartAdminLogic();
                    break;
                case 3:
                    DropProduct();
                    break;
                default:
                    Console.WriteLine("Coś poszło nie tak!");
                    Thread.Sleep(2500);
                    StartAdminLogic();
                    break;
            }


        }
        /// <summary>
        /// Delete product record from database
        /// </summary>
        public static void DeleteProduct()
        {
            int input = -1;
            bool correctInput = false;
            int actionChoice = -1;
            Console.Clear();
            ProductsDatabase products = new ProductsDatabase();
            Console.WriteLine(products.ShowAdmin());


            do
            {
                Console.WriteLine("Wybierz produkt który ilość chcesz usunąć " + adminName);
                try
                {
                    input = int.Parse(Console.ReadLine());
                    if (input > 0)
                    {
                        correctInput = true;
                    }
                    else { correctInput = false; }

                }
                catch (System.FormatException)
                {
                    correctInput = false;
                }
            } while (!correctInput);            

            Console.WriteLine(adminName + " Czy chcesz usunąć produkt numer " + input + "?\n" + "1 - Tak\n" + "2 - Nie\n" + "3 - Chce usunąć inny produkt");
            do
            {
                try
                {
                    actionChoice = int.Parse(Console.ReadLine());
                    if (actionChoice > 0)
                    {
                        correctInput = true;
                    }
                    else { correctInput = false; }
                }
                catch (System.FormatException)
                {
                    correctInput = false;
                }
            }while (!correctInput);

            switch (actionChoice)
            {
                case 1:
                    ProductsDatabase.DeleteProduct(input, products);
                    Console.WriteLine("Skasowano");
                    Thread.Sleep(2500);
                    StartAdminLogic();
                    break;
                case 2:
                    StartAdminLogic();
                    break;
                case 3:
                    DropProduct();
                    break;
                default:
                    Console.WriteLine("Coś poszło nie tak!");
                    Thread.Sleep(2500);
                    StartAdminLogic();
                    break;
            }

        }
        
        }
    }
