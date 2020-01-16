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
        private long id;
        private string name;
        private double price;
        private long quantity;

        public long Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }
        public long Quantity { get => quantity; set => quantity = value; }
        /// <summary>
        /// Class constructor
        /// </summary>
        public Product(long id, string name, double price, long quantity)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.quantity = quantity;           
        }
    }
}
