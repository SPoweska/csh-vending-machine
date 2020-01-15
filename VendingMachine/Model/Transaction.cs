using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Model
{
    class Transaction
    {
        private Int64 id;
        private string name;
        private string datetime;
        private double price;       

        public Int64 Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }
        public string DateTime { get => datetime; set => name = value; }
        /// <summary>
        /// Class constructor
        /// </summary>
        public Transaction(Int64 id,string datetime, string name, double price )
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.datetime = datetime;
        }

    }
}
