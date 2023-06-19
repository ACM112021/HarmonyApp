using System;
using System.Collections.Generic;
using System.Data.SqlClient;





namespace webapi_01
{
     public class Tutorial
     {
          public int TutorialId { get; set; }
          public string? Title { get; set; }

          public string? Description { get; set; }
          public string? VideoLink { get; set; }

          // public string? PdfFileName { get; set; }

          // public string? SongUrl { get; set; }
          public int TutorialCount { get; set; }

          public Tutorial()
          {
          }

          public Tutorial(string? title, string? description, string? videoLink)
          {
               Title = title;
               Description = description;
               VideoLink = videoLink;
          }

          public Tutorial(int tutorialId, string? title, string? description, string? videoLink)
          {
               TutorialId = tutorialId;
               Title = title;
               Description = description;
               VideoLink = videoLink;
          }


      

          public static List<Tutorial> GetTutorials(SqlConnection sqlConnection, string baseUrl)
          {
               List<Tutorial> tutorials = new List<Tutorial>();

               string sql = "select TutorialId, Title, Description, VideoLink from Tutorial;";
               SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
               sqlCommand.CommandType = System.Data.CommandType.Text;
               SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
               
               while (sqlDataReader.Read())
               {
                    Tutorial tutorial = new Tutorial();

                    tutorial.TutorialId = Convert.ToInt32(sqlDataReader["TutorialId"].ToString());
                    tutorial.Title = sqlDataReader["Title"].ToString();


                    tutorial.Description = sqlDataReader["Description"].ToString();

                    tutorial.VideoLink = sqlDataReader["VideoLink"].ToString();


                    // tutorial.PdfFileName = sqlDataReader["PdfFileName"].ToString();


                    // dynamic URL
                    // tutorial.SongUrl = baseUrl + "\\pdfs\\" + tutorial.PdfFileName;

                    // tutorials.Add(tutorial);
               }

               return tutorials;
          }



          public static List<Tutorial> SearchTutorials(SqlConnection sqlConnection, string search = "", int pageSize = 10, int pageNumber = 1)
          {
               List<Tutorial> tutorials = new List<Tutorial>();

               string sql = "select p.TutorialID, e.Title, e.Description, e.VideoLink, p.[Count] from (select TutorialID, count(*) over () AS [Count] from Tutorial where Description like '%' + @Search + '%' or Title like '%' + @Search + '%' order by TutorialId offset @PageSize * (@PageNumber - 1) rows fetch next @PageSize rows only) p join Tutorial e on p.TutorialId = e.TutorialId order by 1;";

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
                    Tutorial tutorial = new Tutorial();

                    tutorial.TutorialId = Convert.ToInt32(sqlDataReader["TutorialId"].ToString());
                    tutorial.Title = sqlDataReader["Title"].ToString();
                    // duplicate below? 6/15/23 7:09PM
                    tutorial.Title = sqlDataReader["Title"].ToString();

                    
                    tutorial.Description = sqlDataReader["Description"].ToString();
                    
                    tutorial.VideoLink = sqlDataReader["VideoLink"].ToString();

                    //added 12:31pm 6/14/23
                    // tutorial.PdfFileName = sqlDataReader["PdfFileName"].ToString();

                    tutorials.Add(tutorial);
               }

               return tutorials;
          }





     public static int InsertTutorial(Tutorial tutorial, SqlConnection sqlConnection)
          {
               string sql = "insert into Tutorial(Title, Description, VideoLink) values (@Title, @Description, @VideoLink);";

               SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
               sqlCommand.CommandType = System.Data.CommandType.Text;

               SqlParameter paramTitle = new SqlParameter("@Title", tutorial.Title);
               SqlParameter paramDescription = new SqlParameter("@Description", tutorial.Description);
               SqlParameter paramVideoLink = new SqlParameter("@VideoLink", tutorial.VideoLink);



               paramTitle.DbType = System.Data.DbType.String;
               paramDescription.DbType = System.Data.DbType.String;
               paramVideoLink.DbType = System.Data.DbType.String;

               sqlCommand.Parameters.Add(paramTitle);
               sqlCommand.Parameters.Add(paramDescription);
               sqlCommand.Parameters.Add(paramVideoLink);

               int rowsAffected = sqlCommand.ExecuteNonQuery();
               return rowsAffected;
          }





















          public static int UpdateTutorial(Tutorial tutorial, SqlConnection sqlConnection)
          {
               string sql = "update Tutorial set Title = @Title, Description = @Description, VideoLink = @VideoLink where TutorialId = @TutorialId;";


               SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
               sqlCommand.CommandType = System.Data.CommandType.Text;

               SqlParameter paramTitle = new SqlParameter("@Title", tutorial.Title);
               SqlParameter paramDescription = new SqlParameter("@Description", tutorial.Description);
               SqlParameter paramVideoLink = new SqlParameter("@VideoLink", tutorial.VideoLink);
               SqlParameter paramTutorialId = new SqlParameter("@TutorialId", tutorial.TutorialId);

               paramTitle.DbType = System.Data.DbType.String;
               paramDescription.DbType = System.Data.DbType.String;
               paramVideoLink.DbType = System.Data.DbType.String;
               paramTutorialId.DbType = System.Data.DbType.Int32;

               sqlCommand.Parameters.Add(paramTitle);
               sqlCommand.Parameters.Add(paramDescription);
               sqlCommand.Parameters.Add(paramVideoLink);
               sqlCommand.Parameters.Add(paramTutorialId);

               int rowsAffected = sqlCommand.ExecuteNonQuery();
               return rowsAffected;
          }

















          public static int DeleteTutorial(int tutorialId, SqlConnection sqlConnection)
          {
               string sql = "delete from Tutorial where TutorialId = @TutorialId;";

               SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
               sqlCommand.CommandType = System.Data.CommandType.Text;

               SqlParameter paramTutorialId = new SqlParameter("@TutorialId", tutorialId);
               paramTutorialId.DbType = System.Data.DbType.Int32;
               sqlCommand.Parameters.Add(paramTutorialId);

               int rowsAffected = sqlCommand.ExecuteNonQuery();
               return rowsAffected;
          }







          public void ShowTutorial()
          {
               Console.WriteLine($"{TutorialId}, {Title}, {Description}, {VideoLink}");
          }





          public static void ShowTutorials(List<Tutorial> tutorials)
          {
               foreach (Tutorial tutorial in tutorials)
               {
                    tutorial.ShowTutorial();
               }
          }

     }
}