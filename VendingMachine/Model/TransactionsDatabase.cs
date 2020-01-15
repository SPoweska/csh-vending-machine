using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Threading;

namespace VendingMachine.Model
/// <summary>
/// Database of the transactions 
/// Implements method for fetching transactions by its id
/// </summary>
{
    class TransactionsDatabase
    {
        private List<Transaction> transactions;

        public List<Transaction> Transactions { get => transactions; }

        public TransactionsDatabase()
        {
            transactions = new List<Transaction>();
            //read from database
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=VMbaza.db;Version=3;New=False;Compress=True;"))
            {
                DataTable dt = new DataTable();
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Transactions";
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd.CommandText, conn);
                da.Fill(dt);
                transactions = dt.AsEnumerable().Select(e => new Transaction(e.Field<Int64>(0), e.Field<string>(1), e.Field<string>(2),e.Field<double>(3))).ToList();
            }
        }
                
        /// <summary>
        /// Returns text with all products
        /// </summary>
        /// <returns>Text with all products</returns>
        public string ShowTransactions()
        {
            Console.Clear();
            string output = "\n";
            foreach (var trans in transactions)
            {
                output += trans.Id + " " +trans.DateTime+" "+ trans.Name + " Cena: " + trans.Price + "zł";
                output += "\n";
            }
            return output;
        }
        public static void AddTransaction(int id)
        {
            ProductsDatabase pd = new ProductsDatabase();            
            int number = id;                        
            var prod=pd.FetchProductById(number);
            string name=prod.Name;
            double price = prod.Price;
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=VMbaza.db;Version=3;New=False;Compress=True;"))
                    {
                        conn.Open();
                        SQLiteCommand cmd = conn.CreateCommand();
                        cmd.CommandText = "INSERT INTO Transactions (DateTime,Name,Price) VALUES (datetime('now'),'"+name+"','"+price+"')";
                        cmd.ExecuteNonQuery();
                    }
                
        }
        public static void CleanTransactions()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=VMbaza.db;Version=3;New=False;Compress=True;"))
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM Transactions";
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Pomyślnie skasowano listę transakcji");
            Thread.Sleep(3000);
            AdminLogic.CheckAdminInfo();

        }
        public static void ExportCSV()
        {
            ProductsDatabase pd = new ProductsDatabase();
            DataTable dt = pd.dtable;            

            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dt.Rows)
                {
                 IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                 sb.AppendLine(string.Join(",", fields));
                }

            File.WriteAllText("Transactions.csv", sb.ToString());
            
            Console.WriteLine("Wyeksportowano transakcje pomyślnie");
            Thread.Sleep(3000);
            AdminLogic.CheckAdminInfo();

        }
        

    }
}
