using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Model
{
    /// <summary>
    /// Database of the employees 
    /// Implements method for fetching employee by his id
    /// </summary>
    class EmployeesDatabase
    {
        private List<Employee> employees;

        public List<Employee> Employees { get => employees; }
        /// <summary>
        /// Class constructor
        /// </summary>
        public EmployeesDatabase()
        {
            employees = new List<Employee>();
            //read from database
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=VMbaza.db;Version=3;New=False;Compress=True;"))
            {
                DataTable dt = new DataTable();
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Employers";
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd.CommandText, conn);
                da.Fill(dt);
                employees = dt.AsEnumerable().Select(e => new Employee(e.Field<Int64>(0), e.Field<string>(1), e.Field<string>(2))).ToList();
            }
        }

        /// <summary>
        /// Fetch employee by id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>employee with given id</returns>
        public Employee FetchEmployeeById(int id)
        {
            foreach(var emp in employees)
            {
                if (emp.Id == id)
                    return emp;
            }
            return null;
        }
    }
}
