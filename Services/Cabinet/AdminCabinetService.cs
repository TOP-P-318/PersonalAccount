using ДЗ_на_25_мая_Тимур_Жуков.Models;
using ДЗ_на_25_мая_Тимур_Жуков.Repositories;

namespace ДЗ_на_25_мая_Тимур_Жуков.Services.Cabinet;

public class AdminCabinetService : IAdminCabinetService
{
    private readonly IAccountRepo _accountRepo;
    private readonly IStudentProfileRepo _studentProfileRepo;

    public AdminCabinetService(IAccountRepo accountRepo, IStudentProfileRepo studentProfileRepo)
    {
        _accountRepo = accountRepo;
        _studentProfileRepo = studentProfileRepo;
    }

    public async Task<Dictionary<int, AccountModel>> GetAllStudentAccountsAsync()
    {
        var accounts = await _accountRepo.GetByRoleAsync(AccountRole.Student);
        return accounts.ToDictionary(a => a.Id);
    }

    public async Task<List<StudentProfileModel>> GetAllStudentProfilesAsync()
    {
        return await _studentProfileRepo.GetAllAsync();
    }

    public async Task ConfirmStudentEmailAsync(int accountId)
    {
        await Task.CompletedTask;
    }

    public bool IsEmailConfirmed(int accountId)
    {
        
        return true;
    }
}