
using STUDENTSDB.Models;
namespace STUDENTSDB.Controllers
{
    public interface IStudentsRepository
    {
        Task<List<Students>> GetAllStudentsAsync();
        Task<Students> GetStudentsByIdAsync(int id);
        Task AddStudentsAsync(Students students);
        Task UpdateStudentsAsync(Students students);
        Task DeleteStudentsAsync(int id);
    }
}
