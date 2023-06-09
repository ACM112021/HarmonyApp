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
