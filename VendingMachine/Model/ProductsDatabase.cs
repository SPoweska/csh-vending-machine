using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace VendingMachine.Model
{
    /// <summary>
    /// Database of the products 
    /// Implements method for fetching product by its id
    /// </summary>
    class ProductsDatabase
    {
        private List<Product> products;

        public List<Product> Products { get => products; set => products = value; }

        public ProductsDatabase()
        { 
            products = new List<Product>();
            //read from database
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=VMbaza.db;Version=3;New=False;Compress=True;"))
            {
                DataTable dt = new DataTable();
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Products";
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd.CommandText, conn);
                da.Fill(dt);
                products = dt.AsEnumerable().Select(p => new Product(p.Field<Int64>(0), p.Field<string>(1), p.Field<double>(2), p.Field<Int64>(3), p.Field<Int64>(4))).ToList();
            }
        }

        /// <summary>
        /// Fetch product by its id
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>product with given id</returns>
        public Product FetchProductById(int id)
        {
            foreach (var prod in products)
            {
                if (prod.Id == id)
                    return prod;
            }
            return null;
        }

        /// <summary>
        /// Returns text with all products
        /// </summary>
        /// <returns>Text with all products</returns>
        public string Show()
        {
            string output = "\n";
            foreach(var prod in products)
            {
                output += prod.Id + " " + prod.Name + " Cena: " + prod.Price + "zł";
                output += "\n";
            }
            return output;
        }
    }
}
