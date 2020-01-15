using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

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
            string output = "\n";
            foreach (var trans in transactions)
            {
                output += trans.Id + " " +trans.DateTime+" "+ trans.Name + " Cena: " + trans.Price + "zł";
                output += "\n";
            }
            return output;
        }
        

    }
}
