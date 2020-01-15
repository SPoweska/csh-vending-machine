using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Threading;

namespace VendingMachine.Model
{
    /// <summary>
    /// Database of the products 
    /// Implements method for fetching product by its id
    /// </summary>
    class ProductsDatabase
    {
        public List<Product> products;
        public DataTable dtable;

        public List<Product> Products { get => products; set => products = value; }

        public ProductsDatabase()
        {
            products = new List<Product>();
            //read from database
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=VMbaza.db;Version=3;New=False;Compress=True;"))
            {
                dtable = new DataTable();
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Products";
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd.CommandText, conn);
                da.Fill(dtable);
                products = dtable.AsEnumerable().Select(p => new Product(p.Field<Int64>(0), p.Field<string>(1),
                    p.Field<double>(2), p.Field<Int64>(3))).ToList();
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

        public static void AddNewProduct(string name, float price, int quantity)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=VMbaza.db;Version=3;New=False;Compress=True;"))
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO Products (Name,Price,Quantity) VALUES ('" + name + "','" + price + "','" + quantity + "')";
                cmd.ExecuteNonQuery();

            }

        }

        /// <summary>
        /// Returns text with all products
        /// </summary>
        /// <returns>Text with all products</returns>
        public string Show()
        {
            string output = "\n";
            foreach (var prod in products)
            {
                output += prod.Id + " " + prod.Name + " Cena: " + prod.Price + "zł";                
                output += "\n";
            }
            return output;
        }

        /// <summary>
        /// Returns text with all products
        /// </summary>
        /// <returns>Text with all products</returns>
        public string ShowAdmin()
        {
            string output = "\n";
            foreach (var prod in products)
            {
                output += prod.Id + " " + prod.Name + " Ilość: " + prod.Quantity;
                output += "\n";
            }
            return output;
        }
        public static void Increment(int id,int quantity, ProductsDatabase product)
        {

            int length = product.Products.Count;
            int increment = quantity;

            if (length >= id)
            {
                for (int i = 1; i <= length; i++)
                {
                    if (i == id)
                    {
                        //increment choosed product quantity                    
                        using (SQLiteConnection conn = new SQLiteConnection("Data Source=VMbaza.db;Version=3;New=False;Compress=True;"))
                        {
                            conn.Open();
                            SQLiteCommand cmd = conn.CreateCommand();
                            cmd.CommandText = "UPDATE Products SET Quantity = Quantity + '" + increment + "' WHERE ID='" + id + "'";
                            cmd.ExecuteNonQuery();

                        }
                    }
                }
            }
            else
            {

            }
        }
        public static void Decrement(int id, ProductsDatabase product)
        {
            int length = product.Products.Count;
            int decrement = 1;
            if (id<=length)
            {
                for (int i = 1; i <= length; i++)
                {
                    if (i == id)
                    {
                        //decrement choosed product quantity                    
                        using (SQLiteConnection conn = new SQLiteConnection("Data Source=VMbaza.db;Version=3;New=False;Compress=True;"))
                        {
                            conn.Open();
                            SQLiteCommand cmd = conn.CreateCommand();
                            cmd.CommandText = "UPDATE Products SET Quantity = Quantity - '" + decrement + "' WHERE ID='" + id + "'";
                            cmd.ExecuteNonQuery();

                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Wybrano niewłaściwy numer");
                Pause();
                MachineLogic.ChooseProduct(product);
            }
        }
        public static void DropQuantity(int id, int quantity, ProductsDatabase product)
        {
            int length = product.Products.Count;
            int drop = quantity;

            if (length >= id)
            {
                for (int i = 1; i <= length; i++)
                {
                    if (i == id)
                    {
                        //drop choosed product quantity                    
                        using (SQLiteConnection conn = new SQLiteConnection("Data Source=VMbaza.db;Version=3;New=False;Compress=True;"))
                        {
                            conn.Open();
                            SQLiteCommand cmd = conn.CreateCommand();
                            cmd.CommandText = "UPDATE Products SET Quantity = Quantity - '" + drop + "' WHERE ID='" + id + "'";
                            cmd.ExecuteNonQuery();

                        }
                    }
                }
            }
            else
            {

            }
        }
        public static void DeleteProduct(int id, ProductsDatabase product)
        {
            int length = product.Products.Count;
            int delete = id;

            if (length >= id)
            {
                for (int i = 1; i <= length; i++)
                {
                    if (i == id)
                    {            
                        using (SQLiteConnection conn = new SQLiteConnection("Data Source=VMbaza.db;Version=3;New=False;Compress=True;"))
                        {
                            conn.Open();
                            SQLiteCommand cmd = conn.CreateCommand();
                            cmd.CommandText = "DELETE FROM Products WHERE ID='" + id + "';UPDATE Products SET ID = ID-1 WHERE ID >='"+id+"'";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            else
            {

            }

        }
        private static void Pause()
        {
            Thread.Sleep(3000);
        }

    }


       

}

