using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace VendingMachine.Model
{
    class ProductsDatabase
    {
        private List<Product> products;

        public List<Product> Products { get => products; set => products = value; }

        public ProductsDatabase()
        { 
            products = new List<Product>();
            //using (SQLiteConnection conn = new SQLiteConnection("Data Source=VMbaza.db;Version=3;New=False;Compress=True;"))
            //{
            //    conn.Open();
            //    using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Products"))
            //    {
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //        using (SQLiteDataReader reader = cmd.ExecuteReader())
            //        {
            //            while(reader.Read())
            //            {
            //                products.Add(new Product(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4)));
            //            }
            //        }
            //    }
            //    foreach(var product in products)
            //    {
            //        Console.WriteLine(product.Name);
            //    }
            //}
            //read from database
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=VMbaza.db;Version=3;New=False;Compress=True;"))
            {
                DataTable dt = new DataTable();
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Products";
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd.CommandText, conn);
                da.Fill(dt);
                //List<long> products = dt.AsEnumerable().Select(r => r.Field<Int64>(0)).ToList();
                products = dt.AsEnumerable().Select(p => new Product(p.Field<Int64>(0), p.Field<string>(1), p.Field<double>(2), p.Field<Int64>(3), p.Field<Int64>(4))).ToList();
            }
        }

        //private List<Product> GetListByDataTable(DataTable dt)
        //{
        //    var reult = (from rw in dt.AsEnumerable()
        //                 select new
        //                 {
        //                     Name = Convert.ToString(rw["Name"]),
        //                     ID = Convert.ToInt32(rw["ID"])
        //                 }).ToList();



        //    return reult.ConvertAll<Product>(o => (Product)o);
        //}

    }
}
