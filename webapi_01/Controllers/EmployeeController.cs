using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Web;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Cors;



// namespace webapi_01.Controllers;

// [ApiController]
// [Route("[controller]")]
// public class EmployeeController : ControllerBase
// {
//      private readonly ILogger<EmployeeController> _logger;
//      public EmployeeController(ILogger<EmployeeController> logger)
//      {
//           _logger = logger;
//      }


//      [HttpGet]
//      [Route("/getemployees")]
//      public List<Employee> GetEmployees()
//      {
//           List<Employee> employees = new List<Employee>();

//           string connectionString = GetConnectionString();
//           using (SqlConnection sqlConnection = new SqlConnection(connectionString))
//           {
//                sqlConnection.Open();
//                employees = Employee.GetEmployees(sqlConnection);
//           }

//           return employees;
//      }


//      // 5/20/23 Saturday 11:34am
//      // asked GPT for interpreting 18-12-G instructions, very unclear
//      // current insertEmployee Method below as of 11:45am:

//      // [HttpGet]
//      // [Route("/InsertEmployee")]
//      // public List<Employee> InsertEmployee(string lastName, string firstName, decimal salary)
//      // {
//      //      List<Employee> employees;

//      //      using (SqlConnection sqlConnection = new SqlConnection(connectionString))
//      //      {
//      //           sqlConnection.Open();
//      //           Employee.InsertEmployee(lastName, firstName, salary, sqlConnection);
//      //           employees = Employee.GetEmployees(sqlConnection);
//      //      }

//      //      return employees;
//      // }


//      // Wurfi's method below:


//      [HttpGet]
//      [Route("/InsertEmployee")]
//      public List<Employee> InsertEmployee(string lastName, string firstName, decimal salary)
//      {
//           string connectionString = GetConnectionString();
//           using (SqlConnection sqlConnection = new SqlConnection(connectionString))
//           {
//                sqlConnection.Open();
//                Employee.InsertEmployee(lastName, firstName, salary, sqlConnection);
//                return Employee.GetEmployees(sqlConnection);
//           }
//      }


//      // used to be HttpDelete, switched to HttpGet advised by Curfi
//      [HttpGet]
//      [Route("/DeleteEmployee")]

//      public List<Employee> DeleteEmployee(int employeeId)
//      {
//           string connectionString = GetConnectionString();
//           using (SqlConnection sqlConnection = new SqlConnection(connectionString))
//           {
//                sqlConnection.Open();
//                Employee.DeleteEmployee(employeeId, sqlConnection);
//                return Employee.GetEmployees(sqlConnection);
//           }
//      }

//      static string GetConnectionString()
//      {
//           string serverName = @"DESKTOP-RBF3DB2\SQLEXPRESS"; //Change to the "Server Name" you see when you launch SQL Server Management Studio.

//           string databaseName = "db01"; //Change to the database where you created your Employee table.

//           string connectionString = $"data source={serverName}; database={databaseName}; Integrated Security=true;";
//           return connectionString;
//      }
// }


// commented out all above Monday 6/5/23 1:16PM, from hw
// replacement code below:























// the [EnableCors("AllowOrigin")] part breaks the code, don't un-comment it
// 6/7/23 1:46PM




namespace webapi_01.Controllers;


[ApiController]
[Route("[controller]")]
// [EnableCors("AllowOrigin")]
public class EmployeeController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public EmployeeController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("/SearchEmployees")]
    public Response SearchEmployees(string pageSize = "10", string pageNumber = "1", string search = "")
    {
        Response response = new Response();
        try
        {
            List<Employee> employees = new List<Employee>();

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                employees = Employee.SearchEmployees(sqlConnection, search, Convert.ToInt32(pageSize), Convert.ToInt32(pageNumber));
            }

            string message = "";

            if (employees.Count() > 0)
            {
                int employeeCount = employees[0].EmployeeCount;
                message = $"Found {employeeCount} employees!";
            }
            else
            {
                message = "No employees met your search criteria.";
            }

            response.Result = "success";
            response.Message = message;
            response.Employees = employees;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }
        return response;
    }

    [HttpGet]
    [Route("/InsertEmployee")]
    public Response InsertEmployee(string lastName, string firstName, string salary)
    {
        Response response = new Response();
        try
        {
            List<Employee> employees = new List<Employee>();

            Employee employee = new Employee(lastName, firstName, Convert.ToDecimal(salary));

            int rowsAffected = 0;

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                rowsAffected = Employee.InsertEmployee(employee, sqlConnection);
                employees = Employee.SearchEmployees(sqlConnection);
            }

            response.Result = (rowsAffected == 1) ? "success" : "failure";
            response.Message = $"{rowsAffected} rows affected.";
            response.Employees = employees;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }

        return response;
    }

    [HttpGet]
    [Route("/UpdateEmployee")]
    public Response UpdateEmployee(string employeeId, string lastName, string firstName, string salary)
    {
        Response response = new Response();

        try
        {
            List<Employee> employees = new List<Employee>();
            Employee employee = new Employee(Convert.ToInt32(employeeId), lastName, firstName, Convert.ToDecimal(salary));

            int rowsAffected = 0;

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                rowsAffected = Employee.UpdateEmployee(employee, sqlConnection);
                employees = Employee.SearchEmployees(sqlConnection);
            }

            response.Result = (rowsAffected == 1) ? "success" : "failure";
            response.Message = $"{rowsAffected} rows affected.";
            response.Employees = employees;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }

        return response;
    }

    [HttpGet]
    [Route("/DeleteEmployee")]
    public Response DeleteEmployee(string employeeId)
    {
        Response response = new Response();

        try
        {
            List<Employee> employees = new List<Employee>();
            int rowsAffected = 0;

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                rowsAffected = Employee.DeleteEmployee(Convert.ToInt32(employeeId), sqlConnection);
                employees = Employee.SearchEmployees(sqlConnection);
            }

            response.Result = (rowsAffected == 1) ? "success" : "failure";
            response.Message = $"{rowsAffected} rows affected.";
            response.Employees = employees;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }

        return response;
    }

    static string GetConnectionString()
    {
        string serverName = @"DESKTOP-RBF3DB2\SQLEXPRESS"; //Change to the "Server Name" you see when you launch SQL Server Management Studio.
        string databaseName = "db01"; //Change to the database where you created your Employee table.
        string connectionString = $"data source={serverName}; database={databaseName}; Integrated Security=true;";
        return connectionString;
    }

}































// namespace webapi_01.Controllers;

// [ApiController]
// [Route("[controller]")]
// public class EmployeeController : ControllerBase
// {
//     private readonly ILogger<WeatherForecastController> _logger;

//     public EmployeeController(ILogger<WeatherForecastController> logger)
//     {
//         _logger = logger;
//     }

//     [HttpGet]
//     [Route("/SearchEmployees")]
//     public Response SearchEmployees(string pageSize = "10", string pageNumber = "1", string search = "")
//     {
//         Response response = new Response();
//         try
//         {
//             List<Employee> employees = new List<Employee>();

//             string connectionString = GetConnectionString();
//             using (SqlConnection sqlConnection = new SqlConnection(connectionString))
//             {
//                 sqlConnection.Open();
//                 employees = Employee.SearchEmployees(sqlConnection, search, Convert.ToInt32(pageSize), Convert.ToInt32(pageNumber));
//             }

//             string message = "";

//             if (employees.Count() > 0)
//             {
//                 int employeeCount = employees[0].EmployeeCount;
//                 message = $"Found {employeeCount} employees!";
//             }
//             else
//             {
//                 message = "No employees met your search criteria.";
//             }

//             response.Result = "success";
//             response.Message = message;
//             response.Employees = employees;
//         }
//         catch (Exception e)
//         {
//             response.Result = "failure";
//             response.Message = e.Message;
//         }
//         return response;
//     }

//     [HttpGet]
//     [Route("/InsertEmployee")]
//     public Response InsertEmployee(string lastName, string firstName, string salary)
//     {
//         Response response = new Response();
//         try
//         {
//             List<Employee> employees = new List<Employee>();

//             Employee employee = new Employee(lastName, firstName, Convert.ToDecimal(salary));

//             int rowsAffected = 0;

//             string connectionString = GetConnectionString();
//             using (SqlConnection sqlConnection = new SqlConnection(connectionString))
//             {
//                 sqlConnection.Open();
//                 rowsAffected = Employee.InsertEmployee(employee, sqlConnection);
//                 employees = Employee.SearchEmployees(sqlConnection);
//             }

//             response.Result = (rowsAffected == 1) ? "success" : "failure";
//             response.Message = $"{rowsAffected} rows affected.";
//             response.Employees = employees;
//         }
//         catch (Exception e)
//         {
//             response.Result = "failure";
//             response.Message = e.Message;
//         }

//         return response;
//     }

//     [HttpGet]
//     [Route("/UpdateEmployee")]
//     public Response UpdateEmployee(string employeeId, string lastName, string firstName, string salary)
//     {
//         Response response = new Response();

//         try
//         {
//             List<Employee> employees = new List<Employee>();
//             Employee employee = new Employee(Convert.ToInt32(employeeId), lastName, firstName, Convert.ToDecimal(salary));

//             int rowsAffected = 0;

//             string connectionString = GetConnectionString();
//             using (SqlConnection sqlConnection = new SqlConnection(connectionString))
//             {
//                 sqlConnection.Open();
//                 rowsAffected = Employee.UpdateEmployee(employee, sqlConnection);
//                 employees = Employee.SearchEmployees(sqlConnection);
//             }

//             response.Result = (rowsAffected == 1) ? "success" : "failure";
//             response.Message = $"{rowsAffected} rows affected.";
//             response.Employees = employees;
//         }
//         catch (Exception e)
//         {
//             response.Result = "failure";
//             response.Message = e.Message;
//         }

//         return response;
//     }

//     [HttpGet]
//     [Route("/DeleteEmployee")]
//     public Response DeleteEmployee(string employeeId)
//     {
//         Response response = new Response();

//         try
//         {
//             List<Employee> employees = new List<Employee>();
//             int rowsAffected = 0;

//             string connectionString = GetConnectionString();
//             using (SqlConnection sqlConnection = new SqlConnection(connectionString))
//             {
//                 sqlConnection.Open();
//                 rowsAffected = Employee.DeleteEmployee(Convert.ToInt32(employeeId), sqlConnection);
//                 employees = Employee.SearchEmployees(sqlConnection);
//             }

//             response.Result = (rowsAffected == 1) ? "success" : "failure";
//             response.Message = $"{rowsAffected} rows affected.";
//             response.Employees = employees;
//         }
//         catch (Exception e)
//         {
//             response.Result = "failure";
//             response.Message = e.Message;
//         }

//         return response;
//     }

//     static string GetConnectionString()
//     {
//         string serverName = @"DESKTOP-RBF3DB2\SQLEXPRESS"; //Change to the "Server Name" you see when you launch SQL Server Management Studio.
//         string databaseName = "db01"; //Change to the database where you created your Employee table.
//         string connectionString = $"data source={serverName}; database={databaseName}; Integrated Security=true;";
//         return connectionString;
//     }

// }