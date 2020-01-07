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
        private const string notAcceptedNominal = "Włożono nie akceptowalny nominał";

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
                Output(nominals);
                float money;
                try
                {
                    money = float.Parse(Input());
                }
                catch (System.FormatException ex)
                {
                    continue;
                }
                bool accepted = false;
                foreach(var price in acceptedNominals)
                {
                    if (price == money)
                        accepted = true;
                }
                if (accepted == false)
                {
                    Output(notAcceptedNominal);
                    continue;
                }
            } while (!end);
            Pause();
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
