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
public class MusicSheetController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public MusicSheetController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("/SearchMusicSheets")]
    public Response SearchMusicSheets(string pageSize = "10", string pageNumber = "1", string search = "")
    {
        Response response = new Response();
        try
        {
            List<MusicSheet> musicSheets = new List<MusicSheet>();

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                musicSheets = MusicSheet.SearchMusicSheets(sqlConnection, search, Convert.ToInt32(pageSize), Convert.ToInt32(pageNumber));
            }

            string message = "";

            if (musicSheets.Count() > 0)
            {
                int musicSheetCount = musicSheets[0].MusicSheetCount;
                message = $"Found {musicSheetCount} music sheets!";
            }
            else
            {
                message = "No music sheets met your search criteria.";
            }

            response.Result = "success";
            response.Message = message;
            response.MusicSheets = musicSheets;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }
        return response;
    }







 // here @ Friday 6/9/23 9:31pm








    [HttpGet]
    [Route("/InsertMusicSheet")]
    public Response InsertMusicSheet(string songTitle, string startDate, string completedDate)
    {
        Response response = new Response();
        try
        {
            List<MusicSheet> musicSheets = new List<MusicSheet>();

            MusicSheet musicSheet = new MusicSheet(songTitle, startDate, completedDate);

            int rowsAffected = 0;

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                rowsAffected = MusicSheet.InsertMusicSheet(musicSheet, sqlConnection);
                musicSheets = MusicSheet.SearchMusicSheets(sqlConnection);
            }

            response.Result = (rowsAffected == 1) ? "success" : "failure";
            response.Message = $"{rowsAffected} rows affected.";
            response.MusicSheets = musicSheets;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }

        return response;
    }




















    [HttpGet]
    [Route("/UpdateMusicSheet")]
    public Response UpdateMusicSheet(string musicSheetId, string songTitle, string startDate, string completedDate)
    {
        Response response = new Response();

        try
        {
            List<MusicSheet> musicSheets = new List<MusicSheet>();
            MusicSheet musicSheet = new MusicSheet(Convert.ToInt32(musicSheetId), songTitle, startDate, completedDate);

            int rowsAffected = 0;

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                rowsAffected = MusicSheet.UpdateMusicSheet(musicSheet, sqlConnection);
                musicSheets = MusicSheet.SearchMusicSheets(sqlConnection);
            }

            response.Result = (rowsAffected == 1) ? "success" : "failure";
            response.Message = $"{rowsAffected} rows affected.";
            response.MusicSheets = musicSheets;
        }
        catch (Exception e)
        {
            response.Result = "failure";
            response.Message = e.Message;
        }

        return response;
    }



















    [HttpGet]
    [Route("/DeleteMusicSheet")]
    public Response DeleteMusicSheet(string musicSheetId)
    {
        Response response = new Response();

        try
        {
            List<MusicSheet> musicSheets = new List<MusicSheet>();
            int rowsAffected = 0;

            string connectionString = GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                rowsAffected = MusicSheet.DeleteMusicSheet(Convert.ToInt32(musicSheetId), sqlConnection);
                musicSheets = MusicSheet.SearchMusicSheets(sqlConnection);
            }

            response.Result = (rowsAffected == 1) ? "success" : "failure";
            response.Message = $"{rowsAffected} rows affected.";
            response.MusicSheets = musicSheets;
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
