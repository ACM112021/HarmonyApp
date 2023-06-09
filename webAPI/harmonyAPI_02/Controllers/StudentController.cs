using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Collections.Generic;




namespace webapi_01.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
     private readonly ILogger<StudentController> _logger;

     public StudentController(ILogger<StudentController> logger)
     {
          _logger = logger;
     }

     [HttpGet]
     [Route("/SearchStudents")]
     public Response SearchStudents(string pageSize = "10", string pageNumber = "1", string search = "")
     {
          Response response = new Response();
          try
          {
               List<Student> students = new List<Student>();

               string connectionString = GetConnectionString();
               using (SqlConnection sqlConnection = new SqlConnection(connectionString))
               {
                    sqlConnection.Open();
                    students = Student.SearchStudents(sqlConnection, search, Convert.ToInt32(pageSize), Convert.ToInt32(pageNumber));
               }

               string message = "";

               if (students.Count() > 0)
               {
                    int studentCount = students[0].StudentCount;
                    message = $"Found {studentCount} students!";
               }
               else
               {
                    message = "No students met your search criteria.";
               }

               response.Result = "success";
               response.Message = message;
               response.Students = students;
          }
          catch (Exception e)
          {
               response.Result = "failure";
               response.Message = e.Message;
          }
          return response;
     }

     [HttpGet]
     [Route("/InsertStudent")]
     public Response InsertStudent(string lastName, string firstName, string studentBalance)
     {
          Response response = new Response();
          try
          {
               List<Student> students = new List<Student>();

               Student student = new Student(lastName, firstName, Convert.ToDecimal(studentBalance));

               int rowsAffected = 0;

               string connectionString = GetConnectionString();
               using (SqlConnection sqlConnection = new SqlConnection(connectionString))
               {
                    sqlConnection.Open();
                    rowsAffected = Student.InsertStudent(student, sqlConnection);
                    students = Student.SearchStudents(sqlConnection);
               }

               response.Result = (rowsAffected == 1) ? "success" : "failure";
               response.Message = $"{rowsAffected} rows affected.";
               response.Students = students;
          }
          catch (Exception e)
          {
               response.Result = "failure";
               response.Message = e.Message;
          }

          return response;
     }

     [HttpGet]
     [Route("/UpdateStudent")]
     public Response UpdateStudent(string studentId, string lastName, string firstName, string studentBalance)
     {
          Response response = new Response();

          try
          {
               List<Student> students = new List<Student>();
               Student student = new Student(Convert.ToInt32(studentId), lastName, firstName, Convert.ToDecimal(studentBalance));

               int rowsAffected = 0;

               string connectionString = GetConnectionString();
               using (SqlConnection sqlConnection = new SqlConnection(connectionString))
               {
                    sqlConnection.Open();
                    rowsAffected = Student.UpdateStudent(student, sqlConnection);
                    students = Student.SearchStudents(sqlConnection);
               }

               response.Result = (rowsAffected == 1) ? "success" : "failure";
               response.Message = $"{rowsAffected} rows affected.";
               response.Students = students;
          }
          catch (Exception e)
          {
               response.Result = "failure";
               response.Message = e.Message;
          }

          return response;
     }

     [HttpGet]
     [Route("/DeleteStudent")]
     public Response DeleteStudent(string studentId)
     {
          Response response = new Response();

          try
          {
               List<Student> students = new List<Student>();
               int rowsAffected = 0;

               string connectionString = GetConnectionString();
               using (SqlConnection sqlConnection = new SqlConnection(connectionString))
               {
                    sqlConnection.Open();
                    rowsAffected = Student.DeleteStudent(Convert.ToInt32(studentId), sqlConnection);
                    students = Student.SearchStudents(sqlConnection);
               }

               response.Result = (rowsAffected == 1) ? "success" : "failure";
               response.Message = $"{rowsAffected} rows affected.";
               response.Students = students;
          }
          catch (Exception e)
          {
               response.Result = "failure";
               response.Message = e.Message;
          }

          return response;
     }

     [HttpGet]
     [Route("/test")]
     public string TestEndpoint()
     {
          return "Hello World";
     }

     static string GetConnectionString()
     {
          string serverName = @"DESKTOP-RBF3DB2\SQLEXPRESS"; //Change to the "Server Name" you see when you launch SQL Server Management Studio.
          string databaseName = "db01"; //Change to the database where you created your Student table.
          string connectionString = $"data source={serverName}; database={databaseName}; Integrated Security=true;";
          return connectionString;
     }

}