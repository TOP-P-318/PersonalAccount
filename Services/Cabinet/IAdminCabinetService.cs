using ДЗ_на_25_мая_Тимур_Жуков.Models;

namespace ДЗ_на_25_мая_Тимур_Жуков.Services.Cabinet
{
    public interface IAdminCabinetService
    {
        Task<Dictionary<int, AccountModel>> GetAllStudentAccountsAsync();
        Task<List<StudentProfileModel>> GetAllStudentProfilesAsync();
        bool IsEmailConfirmed(int accountId);
        Task ConfirmStudentEmailAsync(int accountId);
    }
}