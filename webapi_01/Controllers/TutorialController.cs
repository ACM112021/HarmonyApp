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
public class TutorialController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public TutorialController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    

    

    [HttpGet]
    [Route("/SearchTutorials")]
    public Response SearchTutorials(string pageSize = "10", string pageNumber = "1", string search = "")
    {
        Response response = new Response();
        try
        {
            List<Tutorial> tutorials = new List<Tutorial>();

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                tutorials = Tutorial.SearchTutorials(sqlConnection, search, Convert.ToInt32(pageSize), Convert.ToInt32(pageNumber));
            }


            string message = "";

            if (tutorials.Count() > 0)
            {
                int tutorialCount = tutorials[0].TutorialCount;
                message = $"Found {tutorialCount} tutorials!";
            }
            else
            {
                message = "No tutorials met your search criteria.";
            }

            response.Result = "success";
            response.Message = message;
            response.Tutorials = tutorials;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }
        return response;
    }











    [HttpGet]
    [Route("/InsertTutorial")]
    public Response InsertTutorial(string title = "", string description = "", string videoLink = "")
    {
        Response response = new Response();
        try
        {
            List<Tutorial> tutorials = new List<Tutorial>();



            Tutorial tutorial = new Tutorial(
                title == "" ? null : title, 
                description == "" ? null : description, 
                videoLink == "" ? null : videoLink);

            int rowsAffected = 0;

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                rowsAffected = Tutorial.InsertTutorial(tutorial, sqlConnection);
                tutorials = Tutorial.SearchTutorials(sqlConnection);
            }

            response.Result = (rowsAffected == 1) ? "success" : "failure";
            response.Message = $"{rowsAffected} rows affected.";
            response.Tutorials = tutorials;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }

        return response;
    }




















    [HttpGet]
    [Route("/UpdateTutorial")]
    public Response UpdateTutorial(string tutorialId, string title, string? description, string? videoLink)
    {
        Response response = new Response();

        try
        {
            List<Tutorial> tutorials = new List<Tutorial>();
            Tutorial tutorial = new Tutorial(Convert.ToInt32(tutorialId), title, description, videoLink);

            int rowsAffected = 0;

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                rowsAffected = Tutorial.UpdateTutorial(tutorial, sqlConnection);
                tutorials = Tutorial.SearchTutorials(sqlConnection);
            }

            response.Result = (rowsAffected == 1) ? "success" : "failure";
            response.Message = $"{rowsAffected} rows affected.";
            response.Tutorials = tutorials;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }

        return response;
    }



















    [HttpGet]
    [Route("/DeleteTutorial")]
    public Response DeleteTutorial(string tutorialId)
    {
        Response response = new Response();

        try
        {
            List<Tutorial> tutorials = new List<Tutorial>();
            int rowsAffected = 0;

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                rowsAffected = Tutorial.DeleteTutorial(Convert.ToInt32(tutorialId), sqlConnection);
                tutorials = Tutorial.SearchTutorials(sqlConnection);
            }

            response.Result = (rowsAffected == 1) ? "success" : "failure";
            response.Message = $"{rowsAffected} rows affected.";
            response.Tutorials = tutorials;
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
        string serverName = @"DESKTOP-RBF3DB2\SQLEXPRESS"; 
        string databaseName = "db01"; 
        string connectionString = $"data source={serverName}; database={databaseName}; Integrated Security=true;";
        return connectionString;
    }

}
