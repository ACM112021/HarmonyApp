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











    [HttpGet]
    [Route("/InsertMusicSheet")]
    public Response InsertMusicSheet(string songTitle = "", string startDate = "", string completedDate = "")
    {
        Response response = new Response();
        try
        {
            List<MusicSheet> musicSheets = new List<MusicSheet>();



            // Tuesday 6/13/23 6:26PM: Maybe in order to fix the `0000` date displayed, maybe we could change `null` to "in progress" or something. Wait until PDF file hosting is setup. Check the 'null's in the InsertMusicSheet method in SheetMusic.cs as well. Change may ONLY be needed in the SheetMusic.cs part: if 'null' = "in progress"? We'll see. MAYBE MAYBE even just the display in index.js, line 145..nvm that didn't work.

            MusicSheet musicSheet = new MusicSheet(
                songTitle == "" ? null : songTitle, 
                startDate == "" ? null : Convert.ToDateTime(startDate), 
                completedDate == "" ? null : Convert.ToDateTime(completedDate));

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
    public Response UpdateMusicSheet(string musicSheetId, string songTitle, DateTime? startDate, DateTime? completedDate)
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



    // GET PDF Method

    [HttpGet]
    [Route("{id}/pdfs")]
    public IActionResult GetMusicSheetPdf(int id)
    {
        try
        {
           // File path based on the provided identifier (id)
            string filePath = $"D:\\HarmonyApp\\HarmonyApp\\pdfs\\{id}.pdf";

            // Assuming you have the path to the PDF file, you can read it as bytes
            byte[] pdfBytes = System.IO.File.ReadAllBytes("D:\\HarmonyApp\\HarmonyApp\\pdfs\\{id}.pdf");

            // Return the PDF file as the response
            return File(pdfBytes, "application/pdf");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while retrieving the PDF file");
            return StatusCode(500, "Error while retrieving the PDF file");
        }
    }
    








    static string GetConnectionString()
    {
        string serverName = @"DESKTOP-RBF3DB2\SQLEXPRESS"; 
        string databaseName = "db01"; 
        string connectionString = $"data source={serverName}; database={databaseName}; Integrated Security=true;";
        return connectionString;
    }

}
