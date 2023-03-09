using STUDENTSDB.Models;
using System.Data.SqlClient;

namespace STUDENTSDB.Controllers
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly string _connectionString;

        public StudentsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Students>> GetAllStudentsAsync()
        {
            var students = new List<Students>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("STUDENTSVIEW_SP", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                students.Add(new Students
                {
                    ID = reader.GetInt32(0),
                    REGNO = reader.GetInt32(1),
                    NAME = reader.GetString(2),
                    DOB = reader.GetDateTime(3),
                    DEPT = reader.GetString(4)

                });
            }
            return students;
        }
        //private async Task<SqlDataReader> ConnectionMethod(string storedProcedure)
        //{
        //    using var connection = new SqlConnection(_connectionString);
        //    using var command = new SqlCommand(storedProcedure, connection);
        //    command.CommandType = System.Data.CommandType.StoredProcedure;
        //    await connection.OpenAsync();

        //    using var reader = await command.ExecuteReaderAsync();

        //    return reader;
        //}

        public async Task AddStudentsAsync(Students students)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("STUDENTSCREATE_SP", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@regno", students.REGNO);
            command.Parameters.AddWithValue("@name", students.NAME);
            command.Parameters.AddWithValue("@dob", students.DOB);
            command.Parameters.AddWithValue("@dept", students.DEPT);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
        public async Task UpdateStudentsAsync(Students students)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("STUDENTSUPDATE_SP", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", students.ID);
            command.Parameters.AddWithValue("@regno", students.REGNO);
            command.Parameters.AddWithValue("@name", students.NAME);
            command.Parameters.AddWithValue("@dob", students.DOB);
            command.Parameters.AddWithValue("@dept", students.DEPT);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteStudentsAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("STUDENTSUPDATE1_SP", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<Students> GetStudentsByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("STUDENTSVIEWBYID_SP", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Students
                {
                   
                    ID = reader.GetInt32(0),
                    REGNO = reader.GetInt32(1),
                    NAME = reader.GetString(2),
                    DOB = reader.GetDateTime(3),
                    DEPT = reader.GetString(4)

                };
            }

            return null;
        }
    }
}
