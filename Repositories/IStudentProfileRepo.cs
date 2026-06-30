using ДЗ_на_25_мая_Тимур_Жуков.Models;

namespace ДЗ_на_25_мая_Тимур_Жуков.Repositories
{
    public interface IStudentProfileRepo
    {
        Task<StudentProfileModel?> GetByIdAsync(int id);
        Task<StudentProfileModel?> GetByAccountIdAsync(int accountId);
        Task<List<StudentProfileModel>> GetAllAsync();
        Task CreateAsync(StudentProfileModel profile);
    }
}