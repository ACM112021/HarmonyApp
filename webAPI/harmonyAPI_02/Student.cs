using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace webapi_01
{
    public class Student
    {
        public int StudentId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public decimal StudentBalance { get; set; }
        public int StudentCount { get; set; }

        public Student()
        {
        }

        public Student(string lastName, string firstName, decimal studentBalance)
        {
            LastName = lastName;
            FirstName = firstName;
            StudentBalance = studentBalance;
        }

        public Student(int studentId, string lastName, string firstName, decimal studentBalance)
        {
            StudentId = studentId;
            LastName = lastName;
            FirstName = firstName;
            StudentBalance = studentBalance;
        }

        public static List<Student> GetStudents(SqlConnection sqlConnection)
        {
            List<Student> students = new List<Student>();

            string sql = "select StudentId, LastName, FirstName, StudentBalance from Student;";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Student student = new Student();

                student.StudentId = Convert.ToInt32(sqlDataReader["StudentId"].ToString());
                student.LastName = sqlDataReader["LastName"].ToString();
                student.FirstName = sqlDataReader["FirstName"].ToString();
                student.StudentBalance = Convert.ToDecimal(sqlDataReader["StudentBalance"].ToString() == "" ? "0.00" : sqlDataReader["StudentBalance"].ToString());

                students.Add(student);
            }

            return students;
        }

        public static List<Student> SearchStudents(SqlConnection sqlConnection, string search = "", int pageSize = 10, int pageNumber = 1)
        {
            List<Student> students = new List<Student>();

            string sql = "select p.StudentID, e.FirstName, e.LastName, e.StudentBalance, p.[Count] from (select StudentID, count(*) over () AS [Count] from Student where LastName like '%' + @Search + '%' or FirstName like '%' + @Search + '%' order by StudentId offset @PageSize * (@PageNumber - 1) rows fetch next @PageSize rows only) p join Student e on p.StudentId = e.StudentId order by 1;";

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
                Student student = new Student();

                student.StudentId = Convert.ToInt32(sqlDataReader["StudentId"].ToString());
                student.LastName = sqlDataReader["LastName"].ToString();
                student.FirstName = sqlDataReader["FirstName"].ToString();
                student.StudentBalance = Convert.ToDecimal(sqlDataReader["StudentBalance"].ToString() == "" ? "0.00" : sqlDataReader["StudentBalance"].ToString());
                student.StudentCount = Convert.ToInt32(sqlDataReader["Count"].ToString());

                students.Add(student);
            }

            return students;
        }

        public static int InsertStudent(Student student, SqlConnection sqlConnection)
        {
            string sql = "insert into Student (LastName, FirstName, StudentBalance) values (@LastName, @FirstName, @StudentBalance);";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramLastName = new SqlParameter("@LastName", student.LastName);
            SqlParameter paramFirstName = new SqlParameter("@FirstName", student.FirstName);
            SqlParameter studentBalance = new SqlParameter("@StudentBalance", student.StudentBalance);

            paramLastName.DbType = System.Data.DbType.String;
            paramFirstName.DbType = System.Data.DbType.String;
            studentBalance.DbType = System.Data.DbType.Decimal;

            sqlCommand.Parameters.Add(paramLastName);
            sqlCommand.Parameters.Add(paramFirstName);
            sqlCommand.Parameters.Add(studentBalance);

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }

        public static int UpdateStudent(Student student, SqlConnection sqlConnection)
        {
            string sql = "update Student set LastName = @LastName, FirstName = @FirstName, StudentBalance = @StudentBalance where StudentId = @StudentId;";


            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramLastName = new SqlParameter("@LastName", student.LastName);
            SqlParameter paramFirstName = new SqlParameter("@FirstName", student.FirstName);
            SqlParameter paramStudentBalance = new SqlParameter("@StudentBalance", student.StudentBalance);
            SqlParameter paramStudentId = new SqlParameter("@StudentId", student.StudentId);

            paramLastName.DbType = System.Data.DbType.String;
            paramFirstName.DbType = System.Data.DbType.String;
            paramStudentBalance.DbType = System.Data.DbType.Decimal;
            paramStudentId.DbType = System.Data.DbType.Int32;

            sqlCommand.Parameters.Add(paramLastName);
            sqlCommand.Parameters.Add(paramFirstName);
            sqlCommand.Parameters.Add(paramStudentBalance);
            sqlCommand.Parameters.Add(paramStudentId);

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }

        public static int DeleteStudent(int studentId, SqlConnection sqlConnection)
        {
            string sql = "delete from Student where StudentId = @StudentId;";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            SqlParameter paramStudentId = new SqlParameter("@StudentId", studentId);
            paramStudentId.DbType = System.Data.DbType.Int32;
            sqlCommand.Parameters.Add(paramStudentId);

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            return rowsAffected;
        }

        public void ShowStudent()
        {
            Console.WriteLine($"{StudentId}, {LastName}, {FirstName}, {StudentBalance}");
        }

        public static void ShowStudents(List<Student> students)
        {
            foreach (Student student in students)
            {
                student.ShowStudent();
            }
        }

    }
}