using STUDENTSDB.Models;
using System.Data.SqlClient;

namespace STUDENTSDB.Controllers
{
    public class StudentsRepository1 : IStudentsRepository
    {
        private readonly string _connectionString;

        public StudentsRepository1(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public Task AddStudentsAsync(Students students)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStudentsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Students>> GetAllStudentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Students> GetStudentsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStudentsAsync(Students students)
        {
            throw new NotImplementedException();
        }


        private async Task <SqlDataReader> ConnectionMethod(string storedProcedure)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(storedProcedure, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            return reader;
        }
    }
}
