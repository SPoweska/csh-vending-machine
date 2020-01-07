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
        private static List<float> acceptedNominals;
        private const string welcomeText = "Siema, siema! O tej porze każdy wypić może, wiec dej no 2 złote!";
        private const string nominals = "Akceptowane nominały: 50gr, 1zł, 2zł, 5zł";
        private const string insertCoin = "Wrzuć pinionżek :)";
        private const string notAcceptedNominal = "Włożono nie akceptowalny nominał";
        private const string coinLoop = "1 - wybierz produkt, 2 - dobrodzieju, wrzuć więcej pinionżków";
        private const string chooseError = "Wybrano niewałaściwą akcję, spróbuj jeszcze raz.";

        private static float fullMoney = 0;

        static void Main(string[] args)
        {
            acceptedNominals = new List<float>();
            acceptedNominals.Add(0.5f);
            acceptedNominals.Add(1);
            acceptedNominals.Add(2);
            acceptedNominals.Add(5);
            StartMachine();
        }

        private static void StartMachine()
        {
            bool end = false;
            do {
                Output(welcomeText);
                ShowCredit();
                ProductsDatabase products = new ProductsDatabase();
                Output(products.Show());

                bool moreMoney = true;
                do
                {
                    ShowCredit();
                    Output(nominals);
                    
                    float money;
                    Output(insertCoin);
                    try
                    {
                        money = float.Parse(Input());
                        Console.Clear();
                    }
                    catch (System.FormatException ex)
                    {
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
                    } while (error);
                } while (moreMoney);
                ShowCredit();
                end = true;
            } while (!end);
            Pause();
        }

        private static void ShowCredit()
        {
            Output("Kredyt: " + fullMoney + "zł");
        }

        private static string Input()
        {
            return Console.ReadLine();
        }

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
