using System;
using System.Collections.Generic;
using System.Data.SqlClient;

// namespace webapi_01
// {
//      public class Employee
//      {
//           public int EmployeeId { get; set; }
//           public string? LastName { get; set; }
//           public string? FirstName { get; set; }
//           public decimal? Salary { get; set; }

//           public void ShowEmployee()
//           {
//                Console.WriteLine($"{EmployeeId}, {LastName}, {FirstName}, {Salary}");
//           }

//           public static void ShowEmployees(List<Employee> employees)
//           {
//                foreach (Employee employee in employees)
//                {
//                     employee.ShowEmployee();
//                }
//           }

//           public static List<Employee> GetEmployees(SqlConnection sqlConnection)
//           {
//                List<Employee> employees = new List<Employee>();

//                string sql = "select EmployeeId, LastName, FirstName, Salary from Employee;";

//                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
//                {
//                     sqlCommand.CommandType = System.Data.CommandType.Text;

//                     using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
//                     {
//                          while (sqlDataReader.Read())
//                          {
//                               Employee employee = new Employee();

//                               employee.EmployeeId = Convert.ToInt32(sqlDataReader["EmployeeId"]);
//                               employee.LastName = sqlDataReader["LastName"].ToString();
//                               employee.FirstName = sqlDataReader["FirstName"].ToString();
//                               employee.Salary = (sqlDataReader["Salary"].ToString().Length == 0) ? null : Convert.ToDecimal(sqlDataReader["Salary"]);
//                               employees.Add(employee);
//                          }
//                     }
//                }

//                return employees;
//           }

//           public static void InsertEmployee(string lastName, string firstName, decimal salary, SqlConnection sqlConnection)
//           {
//                string sql = "insert into Employee (LastName, FirstName, Salary) values (@LastName, @FirstName, @Salary);";

//                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
//                {
//                     sqlCommand.CommandType = System.Data.CommandType.Text;

//                     sqlCommand.Parameters.Add("@LastName", System.Data.SqlDbType.VarChar);
//                     sqlCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.VarChar);
//                     sqlCommand.Parameters.Add("@Salary", System.Data.SqlDbType.Decimal);

//                     sqlCommand.Parameters["@LastName"].Value = lastName;
//                     sqlCommand.Parameters["@FirstName"].Value = firstName;
//                     sqlCommand.Parameters["@Salary"].Value = salary;

//                     sqlCommand.ExecuteNonQuery();
//                }
//           }


//           public static void DeleteEmployee(int employeeId, SqlConnection sqlConnection)
//           {
               
//                string deleteCommand = "DELETE FROM Employee WHERE EmployeeId = @Id";

//                // Create the command object
//                using (SqlCommand sqlCommand = new SqlCommand(deleteCommand, sqlConnection))
//                {
//                     // Add the parameter for the user ID
//                     sqlCommand.Parameters.AddWithValue("@Id", employeeId);

//                     // Execute the delete command
//                     int rowsAffected = sqlCommand.ExecuteNonQuery();

//                     System.Console.WriteLine(rowsAffected);
//                }
//           }
//      }
// }



// commented all out above Monday 6/5/23 1:19PM, from hw (missing constructors, updated methods since)

// replacement code below:

namespace webapi_01
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public decimal Salary { get; set; }
        public int EmployeeCount { get; set; }

        public Employee()
        {
        }

        public Employee(string lastName, string firstName, decimal salary)
        {
            LastName = lastName;
            FirstName = firstName;
            Salary = salary;
        }

        public Employee(int employeeId, string lastName, string firstName, decimal salary)
        {
            EmployeeId = employeeId;
            LastName = lastName;
            FirstName = firstName;
            Salary = salary;
        }

        public static List<Employee> GetEmployees(SqlConnection sqlConnection)
        {
            List<Employee> employees = new List<Employee>();

            string sql = "select EmployeeId, LastName, FirstName, Salary from Employee;";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Employee employee = new Employee();

                employee.EmployeeId = Convert.ToInt32(sqlDataReader["EmployeeId"].ToString());
                employee.LastName = sqlDataReader["LastName"].ToString();
                employee.FirstName = sqlDataReader["FirstName"].ToString();
                employee.Salary = Convert.ToDecimal(sqlDataReader["Salary"].ToString() == "" ? "0.00" : sqlDataReader["Salary"].ToString());

                employees.Add(employee);
            }

            return employees;
        }

        public static List<Employee> SearchEmployees(SqlConnection sqlConnection, string search = "", int pageSize = 10, int pageNumber = 1)
        {
            List<Employee> employees = new List<Employee>();

            string sql = "select p.EmployeeID, e.FirstName, e.LastName, e.Salary, p.[Count] from (select EmployeeID, count(*) over () AS [Count] from Employee where LastName like '%' + @Search + '%' or FirstName like '%' + @Search + '%' order by EmployeeId offset @PageSize * (@PageNumber - 1) rows fetch next @PageSize rows only) p join Employee e on p.EmployeeId = e.EmployeeId order by 1;";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramSearch = new SqlParameter("@Search", search);
            SqlParameter paramPageSize = new SqlParameter("@PageSize", pageSize);
            SqlParameter paramPageNumber = new SqlParameter("@PageNumber", pageNumber);

            paramSearch.DbType = System.Data.DbType.String;
            paramPageSize.DbType = System.Data.DbType.Int32;
            paramPageNumber.DbType = System.Data.DbType.Int32;

            sqlCommand.Parameters.Add(paramSearch);
            sqlCommand.Parameters.Add(paramPageSize);
            sqlCommand.Parameters.Add(paramPageNumber);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Employee employee = new Employee();

                employee.EmployeeId = Convert.ToInt32(sqlDataReader["EmployeeId"].ToString());
                employee.LastName = sqlDataReader["LastName"].ToString();
                employee.FirstName = sqlDataReader["FirstName"].ToString();
                employee.Salary = Convert.ToDecimal(sqlDataReader["Salary"].ToString() == "" ? "0.00" : sqlDataReader["Salary"].ToString());
                employee.EmployeeCount = Convert.ToInt32(sqlDataReader["Count"].ToString());

                employees.Add(employee);
            }

            return employees;
        }

        public static int InsertEmployee(Employee employee, SqlConnection sqlConnection)
        {
            string sql = "insert into Employee (LastName, FirstName, Salary) values (@LastName, @FirstName, @Salary);";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramLastName = new SqlParameter("@LastName", employee.LastName);
            SqlParameter paramFirstName = new SqlParameter("@FirstName", employee.FirstName);
            SqlParameter salary = new SqlParameter("@Salary", employee.Salary);

            paramLastName.DbType = System.Data.DbType.String;
            paramFirstName.DbType = System.Data.DbType.String;
            salary.DbType = System.Data.DbType.Decimal;

            sqlCommand.Parameters.Add(paramLastName);
            sqlCommand.Parameters.Add(paramFirstName);
            sqlCommand.Parameters.Add(salary);

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }

        public static int UpdateEmployee(Employee employee, SqlConnection sqlConnection)
        {
            string sql = "update Employee set LastName = @LastName, FirstName = @FirstName, Salary = @Salary where EmployeeId = @EmployeeId;";


            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramLastName = new SqlParameter("@LastName", employee.LastName);
            SqlParameter paramFirstName = new SqlParameter("@FirstName", employee.FirstName);
            SqlParameter paramSalary = new SqlParameter("@Salary", employee.Salary);
            SqlParameter paramEmployeeId = new SqlParameter("@EmployeeId", employee.EmployeeId);

            paramLastName.DbType = System.Data.DbType.String;
            paramFirstName.DbType = System.Data.DbType.String;
            paramSalary.DbType = System.Data.DbType.Decimal;
            paramEmployeeId.DbType = System.Data.DbType.Int32;

            sqlCommand.Parameters.Add(paramLastName);
            sqlCommand.Parameters.Add(paramFirstName);
            sqlCommand.Parameters.Add(paramSalary);
            sqlCommand.Parameters.Add(paramEmployeeId);

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }

        public static int DeleteEmployee(int employeeId, SqlConnection sqlConnection)
        {
            string sql = "delete from Employee where EmployeeId = @EmployeeId;";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramEmployeeId = new SqlParameter("@EmployeeId", employeeId);
            paramEmployeeId.DbType = System.Data.DbType.Int32;
            sqlCommand.Parameters.Add(paramEmployeeId);

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }

        public void ShowEmployee()
        {
            Console.WriteLine($"{EmployeeId}, {LastName}, {FirstName}, {Salary}");
        }

        public static void ShowEmployees(List<Employee> employees)
        {
            foreach (Employee employee in employees)
            {
                employee.ShowEmployee();
            }
        }

    }
}