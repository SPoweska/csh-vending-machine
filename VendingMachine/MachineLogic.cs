﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VendingMachine.Model;
using System.Data.SQLite;

namespace VendingMachine
{
    /// <summary>
    /// All the machine logic including input and output
    /// </summary>
    static class MachineLogic
    {
        #region output texts

        private const string welcomeText = "Siema, siema! O tej porze każdy wypić może, wiec dej no 2 złote!";
        private const string nominals = "Akceptowane nominały: 50gr, 1zł, 2zł, 5zł";
        private const string insertCoin = "Wrzuć pinionżek :)";
        private const string notAcceptedNominal = "Włożono nie akceptowalny nominał";
        private const string coinLoop = "1 - wybierz produkt, 2 - dobrodzieju, wrzuć więcej pinionżków";
        private const string chooseError = "Wybrano niewałaściwą akcję, spróbuj jeszcze raz.";
        private const string chooseProduct = "Wybierz produkt";
        private const string notEnoughMoney = "Nie masz wystarczająco dużo pinionżków";        
        private const string endTransaction = "Oto twoja reszta: ";
        private const string wrongNumber = "Wybrano zły numer!";
        private const string wrongFormat = "To nie jest numer!";

        #endregion

        private static List<float> acceptedNominals;
        private static float fullMoney = 0;
        private static bool isAdmin = false;

        /// <summary>
        /// Setup accepted nopminals and start the machine
        /// </summary>
        public static void SetupMachine()
        {
            acceptedNominals = new List<float>();
            acceptedNominals.Add(0.5f);
            acceptedNominals.Add(1);
            acceptedNominals.Add(2);
            acceptedNominals.Add(5);
            StartMachine();
        }

        /// <summary>
        /// Start machine logic
        /// </summary>
        private static void StartMachine()
        {
            bool end = false;
            do
            {
                ProductsDatabase products = ShowProducts();
                Pay();
                if (isAdmin)
                {
                    AdminLogic.StartAdminLogic();
                }
                ShowCredit();
                Output(products.Show());
                ChooseProduct(products);
                EndTransaction();

                end = true;
            } while (!end);
            Pause();
        }

        /// <summary>
        /// Increment fullMoney by a value given by the user
        /// </summary>
        private static void Pay()
        {
            bool moreMoney = true;
            do
            {
                ShowCredit();
                Output(nominals);

                float money;
                Output(insertCoin);
                string input = "";
                try
                {
                    input = Input();
                    money = float.Parse(input);
                    Console.Clear();
                }
                catch (System.FormatException ex)
                {
                    if (AdminLogic.IsAdmin(input))
                    {
                        Console.Clear();
                        isAdmin = true;
                        return;
                    }
                    Console.Clear();
                    continue;
                }
                bool accepted = false;
                foreach (var price in acceptedNominals)
                {
                    if (price == money)
                    {
                        accepted = true;
                        fullMoney += money;
                    }
                }
                if (accepted == false)
                {
                    ShowCredit();
                    Output(nominals);
                    Output(notAcceptedNominal);
                    continue;
                }

                moreMoney = MoreMoneyInput(moreMoney);

            } while (moreMoney);
        }

        /// <summary>
        /// Output user money info
        /// </summary>
        private static void ShowCredit()
        {
            Output("Kredyt: " + fullMoney + "zł");
        }

        /// <summary>
        /// Choose a product from the database
        /// </summary>
        /// <param name="products">database to choose from</param>
        public static void ChooseProduct(ProductsDatabase products)
        {
            bool choosedProduct = false;
            int prod = 0;
            double productCost;
            int length = products.Products.Count;
            do
            {
                Console.Clear();
                ShowCredit();
                Output(products.Show());
                Output(chooseProduct);
                try
                {
                    prod = int.Parse(Input());
                    if (length >= prod)
                    {
                        choosedProduct = true;
                    }
                    else
                    {
                        choosedProduct = false;
                        Output(wrongNumber);
                        Thread.Sleep(2500);
                    }
                }
                catch (System.FormatException)
                {   
                    choosedProduct = false;
                    Output(wrongFormat);
                    Thread.Sleep(2500);
                }                 
            } while (!choosedProduct);                     
            productCost = products.Products[prod - 1].Price;

            //check if price is lower or equal to credit
            if (productCost > fullMoney)
            {
                Output(notEnoughMoney);
                StartMachine();
            }
            else
            {
                fullMoney = ChangeCalculator.CalculateChange(fullMoney, (float)productCost);
                ProductsDatabase.Decrement(prod, products);
                Console.Clear();
            }
        }

        /// <summary>
        /// Show final text, wait 5 seconds and start the program again
        /// </summary>
        private static void EndTransaction()
        {
            Output(endTransaction + fullMoney + "zł");
            Thread.Sleep(5000);
            fullMoney = 0;
            Console.Clear();
            StartMachine();
        }

        /// <summary>
        /// Shows and returns all products from the database
        /// </summary>
        /// <returns>product database</returns>
        private static ProductsDatabase ShowProducts()
        {
            Output(welcomeText);
            ShowCredit();
            ProductsDatabase products = new ProductsDatabase();
            Output(products.Show());
            return products;
        }

        /// <summary>
        /// Check if user want to give more mone to the credit
        /// </summary>
        /// <param name="moreMoney">did user want to give more money</param>
        /// <returns>does user want to give more money</returns>
        private static bool MoreMoneyInput(bool moreMoney)
        {
            string input;
            bool error = false;
            do
            {
                ShowCredit();
                Output(coinLoop);
                input = Input();
                switch (input[0])
                {
                    case '1':
                        moreMoney = false;
                        break;
                    case '2':
                        moreMoney = true;
                        break;
                    default:
                        Output(chooseError);
                        error = true;
                        break;
                }
                Console.Clear();
            } while (error);
            return moreMoney;
        }

        /// <summary>
        /// Read user input
        /// </summary>
        /// <returns>user input</returns>
        private static string Input()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Pause the console by reading user key
        /// </summary>
        private static void Pause()
        {
            Console.ReadKey();
        }

        /// <summary>
        /// Shows given text to the console
        /// </summary>
        /// <param name="text">text to show</param>
        public static void Output(string text)
        {
            Console.WriteLine(text);
        }
    }
}
