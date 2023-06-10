using System;
using System.Collections.Generic;
using System.Data.SqlClient;



// started updating Friday 6/9/23 6:36pm


namespace webapi_01
{
    public class MusicSheet
    {
        public int MusicSheetId { get; set; }
        public string? SongTitle { get; set; }
        public string? StartDate { get; set; }
        public string? CompletedDate { get; set; }
        public int MusicSheetCount { get; set; }

        public MusicSheet()
        {
        }

        public MusicSheet(string songTitle, string startDate, string completedDate)
        {
            SongTitle = songTitle;
            StartDate = startDate;
            CompletedDate = completedDate;
        }

        public MusicSheet(int musicSheetId, string songTitle, string startDate, string completedDate)
        {
            MusicSheetId = musicSheetId;
            SongTitle = songTitle;
            StartDate = startDate;
            CompletedDate = completedDate;
        }

        public static List<MusicSheet> GetMusicSheets(SqlConnection sqlConnection)
        {
            List<MusicSheet> musicSheets = new List<MusicSheet>();

            string sql = "select MusicSheetId, SongTitle, StartDate, CompletedDate from MusicSheet;";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                MusicSheet musicSheet = new MusicSheet();

                musicSheet.MusicSheetId = Convert.ToInt32(sqlDataReader["MusicSheetId"].ToString());
                musicSheet.SongTitle = sqlDataReader["SongTitle"].ToString();
                musicSheet.StartDate = sqlDataReader["StartDate"].ToString();
                musicSheet.CompletedDate = sqlDataReader["CompletedDate"].ToString();

                musicSheets.Add(musicSheet);
            }

            return musicSheets;
        }

        public static List<MusicSheet> SearchMusicSheets(SqlConnection sqlConnection, string search = "", int pageSize = 10, int pageNumber = 1)
        {
            List<MusicSheet> musicSheets = new List<MusicSheet>();

            string sql = "select p.MusicSheetID, e.SongTitle, e.StartDate, e.CompletedDate, p.[Count] from (select MusicSheetID, count(*) over () AS [Count] from MusicSheet where StartDate like '%' + @Search + '%' or SongTitle like '%' + @Search + '%' order by MusicSheetId offset @PageSize * (@PageNumber - 1) rows fetch next @PageSize rows only) p join MusicSheet e on p.MusicSheetId = e.MusicSheetId order by 1;";

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
                musicSheet.StartDate = sqlDataReader["StartDate"].ToString();
                musicSheet.CompletedDate = sqlDataReader["CompletedDate"].ToString();
                musicSheet.MusicSheetCount = Convert.ToInt32(sqlDataReader["Count"].ToString());

                musicSheets.Add(musicSheet);
            }

            return musicSheets;
        }







        // stopped at this point here updating, Friday 6/9/23 7:31pm




        public static int InsertMusicSheet(MusicSheet musicSheet, SqlConnection sqlConnection)
        {
            string sql = "insert into MusicSheet(SongTitle, StartDate, CompletedDate) values (@SongTitle, @StartDate, @CompletedDate);";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramSongTitle = new SqlParameter("@SongTitle", musicSheet.SongTitle);
            SqlParameter paramStartDate = new SqlParameter("@StartDate", musicSheet.StartDate);
            SqlParameter paramCompletedDate = new SqlParameter("@CompletedDate", musicSheet.CompletedDate);

            paramSongTitle.DbType = System.Data.DbType.String;
            paramStartDate.DbType = System.Data.DbType.String;
            paramCompletedDate.DbType = System.Data.DbType.String;

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