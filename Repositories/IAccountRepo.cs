using ДЗ_на_25_мая_Тимур_Жуков.Models;

namespace ДЗ_на_25_мая_Тимур_Жуков.Repositories
{
    public interface IAccountRepo
    {
        Task<AccountModel?> GetByIdAsync(int id);
        Task<AccountModel?> GetByEmailAsync(string email);
        Task<List<AccountModel>> GetByRoleAsync(AccountRole role);
        Task CreateAsync(AccountModel account);
        Task UpdateAsync(AccountModel account);
    }
}