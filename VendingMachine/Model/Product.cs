using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Model
{
    /// <summary>
    /// Model of a product
    /// </summary>
    class Product
    {
        private Int64 id;
        private string name;
        private double price;
        private Int64 quantity;

        public Int64 Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }
        public Int64 Quantity { get => quantity; set => quantity = value; }
        /// <summary>
        /// Class constructor
        /// </summary>
        public Product(Int64 id, string name, double price, Int64 quantity)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.quantity = quantity;           
        }
    }
}
