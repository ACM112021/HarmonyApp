using System;
using System.Collections.Generic;
using System.Data.SqlClient;





namespace webapi_01
{
     public class MusicSheet
     {
          public int MusicSheetId { get; set; }
          public string? SongTitle { get; set; }

          public DateTime? StartDate { get; set; }
          public DateTime? CompletedDate { get; set; }

          public string? PdfFileName { get; set; }

          public string? SongUrl { get; set; }
          public int MusicSheetCount { get; set; }

          public MusicSheet()
          {
          }

          public MusicSheet(string? songTitle, DateTime? startDate, DateTime? completedDate)
          {
               SongTitle = songTitle;
               StartDate = startDate;
               CompletedDate = completedDate;
          }

          public MusicSheet(int musicSheetId, string? songTitle, DateTime? startDate, DateTime? completedDate)
          {
               MusicSheetId = musicSheetId;
               SongTitle = songTitle;
               StartDate = startDate;
               CompletedDate = completedDate;
          }


          // initial GetMusicSheets method:

          // public static List<MusicSheet> GetMusicSheets(SqlConnection sqlConnection)
          // {
          //      List<MusicSheet> musicSheets = new List<MusicSheet>();

          //      string sql = "select MusicSheetId, SongTitle, StartDate, CompletedDate from MusicSheet;";
          //      SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
          //      sqlCommand.CommandType = System.Data.CommandType.Text;
          //      SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
               
          //      while (sqlDataReader.Read())
          //      {
          //           MusicSheet musicSheet = new MusicSheet();

          //           musicSheet.MusicSheetId = Convert.ToInt32(sqlDataReader["MusicSheetId"].ToString());
          //           musicSheet.SongTitle = sqlDataReader["SongTitle"].ToString();
          //           musicSheet.StartDate = Convert.ToDateTime(sqlDataReader["StartDate"].ToString() == "" ? null : sqlDataReader["StartDate"].ToString());
          //           musicSheet.CompletedDate = Convert.ToDateTime(sqlDataReader["CompletedDate"].ToString() == "" ? null : sqlDataReader["CompletedDate"].ToString());

          //           musicSheets.Add(musicSheet);
          //      }

          //      return musicSheets;
          // }

          // updated GetMusicSheets with PdfFilename property and passing it:

          public static List<MusicSheet> GetMusicSheets(SqlConnection sqlConnection, string baseUrl)
          {
               List<MusicSheet> musicSheets = new List<MusicSheet>();

               string sql = "select MusicSheetId, SongTitle, StartDate, CompletedDate, PdfFileName from MusicSheet;";
               SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
               sqlCommand.CommandType = System.Data.CommandType.Text;
               SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
               
               while (sqlDataReader.Read())
               {
                    MusicSheet musicSheet = new MusicSheet();

                    musicSheet.MusicSheetId = Convert.ToInt32(sqlDataReader["MusicSheetId"].ToString());
                    musicSheet.SongTitle = sqlDataReader["SongTitle"].ToString();
                    musicSheet.StartDate = Convert.ToDateTime(sqlDataReader["StartDate"].ToString() == "" ? null : sqlDataReader["StartDate"].ToString());
                    musicSheet.CompletedDate = Convert.ToDateTime(sqlDataReader["CompletedDate"].ToString() == "" ? null : sqlDataReader["CompletedDate"].ToString());
                    musicSheet.PdfFileName = sqlDataReader["PdfFileName"].ToString();

                    
                    // Tuesday 6/13/23 7:08PM: attempting to define baseUrl while troubleshooting empty links (didn't work)
                    // baseUrl = "http://localhost:5057/";

                    // dynamic URL
                    musicSheet.SongUrl = baseUrl + "\\pdfs\\" + musicSheet.PdfFileName;

                    musicSheets.Add(musicSheet);
               }

               return musicSheets;
          }



          public static List<MusicSheet> SearchMusicSheets(SqlConnection sqlConnection, string search = "", int pageSize = 10, int pageNumber = 1)
          {
               List<MusicSheet> musicSheets = new List<MusicSheet>();

               string sql = "select p.MusicSheetID, e.SongTitle, e.StartDate, e.CompletedDate, e.PdfFileName, p.[Count] from (select MusicSheetID, count(*) over () AS [Count] from MusicSheet where StartDate like '%' + @Search + '%' or SongTitle like '%' + @Search + '%' order by MusicSheetId offset @PageSize * (@PageNumber - 1) rows fetch next @PageSize rows only) p join MusicSheet e on p.MusicSheetId = e.MusicSheetId order by 1;";

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
                    MusicSheet musicSheet = new MusicSheet();

                    musicSheet.MusicSheetId = Convert.ToInt32(sqlDataReader["MusicSheetId"].ToString());
                    musicSheet.SongTitle = sqlDataReader["SongTitle"].ToString();
                    // duplicate below? 6/15/23 7:09PM
                    musicSheet.SongTitle = sqlDataReader["SongTitle"].ToString();
                    musicSheet.StartDate = Convert.ToDateTime(sqlDataReader["StartDate"].ToString() == "" ? null : sqlDataReader["StartDate"].ToString());
                    musicSheet.CompletedDate = Convert.ToDateTime(sqlDataReader["CompletedDate"].ToString() == "" ? null : sqlDataReader["CompletedDate"].ToString());

                    //added 12:31pm 6/14/23
                    musicSheet.PdfFileName = sqlDataReader["PdfFileName"].ToString();

                    musicSheets.Add(musicSheet);
               }

               return musicSheets;
          }









          // attempting to remove time portion from date fields 6/15/23 5:34PM, initial code below


          // public static int InsertMusicSheet(MusicSheet musicSheet, SqlConnection sqlConnection)
          // {
          //      string sql = "insert into MusicSheet(SongTitle, StartDate, CompletedDate) values (@SongTitle, @StartDate, @CompletedDate);";

          //      SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
          //      sqlCommand.CommandType = System.Data.CommandType.Text;

          //      SqlParameter paramSongTitle = new SqlParameter("@SongTitle", musicSheet.SongTitle);
          //      SqlParameter paramStartDate = new SqlParameter("@StartDate", musicSheet.StartDate);
          //      SqlParameter paramCompletedDate = new SqlParameter("@CompletedDate", musicSheet.CompletedDate == null ? (object)DBNull.Value : musicSheet.CompletedDate);


          //      // if (musicSheet.CompletedDate == null)
          //      // {
          //      //      paramCompletedDate = new SqlParameter("@CompletedDate", DBNull.Value);
          //      // }
          //      // else
          //      // {
          //      //      paramCompletedDate = new SqlParameter("@CompletedDate", musicSheet.CompletedDate);
          //      // }


          //      //   if (musicSheet.CompletedDate == "")
          //      //   {
          //      //      paramCompletedDate =  new SqlParameter("@CompletedDate", System.Data.DbType.Date);
          //      //   }

          //      paramSongTitle.DbType = System.Data.DbType.String;
          //      paramStartDate.DbType = System.Data.DbType.DateTime;
          //      paramCompletedDate.DbType = System.Data.DbType.DateTime;

          //      sqlCommand.Parameters.Add(paramSongTitle);
          //      sqlCommand.Parameters.Add(paramStartDate);
          //      sqlCommand.Parameters.Add(paramCompletedDate);

          //      int rowsAffected = sqlCommand.ExecuteNonQuery();
          //      return rowsAffected;
          // }





          // (this new code doesn't break the app, but doesn't remove the time portion like we want. same functionality as before.)
          // attempting to remove time portion from date fields 6/15/23 5:34PM, new code below:





     public static int InsertMusicSheet(MusicSheet musicSheet, SqlConnection sqlConnection)
          {
               string sql = "insert into MusicSheet(SongTitle, StartDate, CompletedDate) values (@SongTitle, @StartDate, @CompletedDate);";

               SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
               sqlCommand.CommandType = System.Data.CommandType.Text;

               SqlParameter paramSongTitle = new SqlParameter("@SongTitle", musicSheet.SongTitle);
               SqlParameter paramStartDate = new SqlParameter("@StartDate", musicSheet.StartDate.Value.Date);
               SqlParameter paramCompletedDate = new SqlParameter("@CompletedDate", musicSheet.CompletedDate.HasValue ? musicSheet.CompletedDate : (object)DBNull.Value);


               // if (musicSheet.CompletedDate == null)
               // {
               //      paramCompletedDate = new SqlParameter("@CompletedDate", DBNull.Value);
               // }
               // else
               // {
               //      paramCompletedDate = new SqlParameter("@CompletedDate", musicSheet.CompletedDate);
               // }


               //   if (musicSheet.CompletedDate == "")
               //   {
               //      paramCompletedDate =  new SqlParameter("@CompletedDate", System.Data.DbType.Date);
               //   }

               paramSongTitle.DbType = System.Data.DbType.String;
               paramStartDate.DbType = System.Data.DbType.Date;
               paramCompletedDate.DbType = System.Data.DbType.Date;

               sqlCommand.Parameters.Add(paramSongTitle);
               sqlCommand.Parameters.Add(paramStartDate);
               sqlCommand.Parameters.Add(paramCompletedDate);

               int rowsAffected = sqlCommand.ExecuteNonQuery();
               return rowsAffected;
          }





















          public static int UpdateMusicSheet(MusicSheet musicSheet, SqlConnection sqlConnection)
          {
               string sql = "update MusicSheet set SongTitle = @SongTitle, StartDate = @StartDate, CompletedDate = @CompletedDate where MusicSheetId = @MusicSheetId;";


               SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
               sqlCommand.CommandType = System.Data.CommandType.Text;

               SqlParameter paramSongTitle = new SqlParameter("@SongTitle", musicSheet.SongTitle);
               SqlParameter paramStartDate = new SqlParameter("@StartDate", musicSheet.StartDate);
               SqlParameter paramCompletedDate = new SqlParameter("@CompletedDate", musicSheet.CompletedDate);
               SqlParameter paramMusicSheetId = new SqlParameter("@MusicSheetId", musicSheet.MusicSheetId);

               paramSongTitle.DbType = System.Data.DbType.String;
               paramStartDate.DbType = System.Data.DbType.String;
               paramCompletedDate.DbType = System.Data.DbType.String;
               paramMusicSheetId.DbType = System.Data.DbType.Int32;

               sqlCommand.Parameters.Add(paramSongTitle);
               sqlCommand.Parameters.Add(paramStartDate);
               sqlCommand.Parameters.Add(paramCompletedDate);
               sqlCommand.Parameters.Add(paramMusicSheetId);

               int rowsAffected = sqlCommand.ExecuteNonQuery();
               return rowsAffected;
          }

















          public static int DeleteMusicSheet(int musicSheetId, SqlConnection sqlConnection)
          {
               string sql = "delete from MusicSheet where MusicSheetId = @MusicSheetId;";

               SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
               sqlCommand.CommandType = System.Data.CommandType.Text;

               SqlParameter paramMusicSheetId = new SqlParameter("@MusicSheetId", musicSheetId);
               paramMusicSheetId.DbType = System.Data.DbType.Int32;
               sqlCommand.Parameters.Add(paramMusicSheetId);

               int rowsAffected = sqlCommand.ExecuteNonQuery();
               return rowsAffected;
          }







          public void ShowMusicSheet()
          {
               Console.WriteLine($"{MusicSheetId}, {SongTitle}, {StartDate}, {CompletedDate}");
          }





          public static void ShowMusicSheets(List<MusicSheet> musicSheets)
          {
               foreach (MusicSheet musicSheet in musicSheets)
               {
                    musicSheet.ShowMusicSheet();
               }
          }

     }
}