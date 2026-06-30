using Microsoft.EntityFrameworkCore;
using ДЗ_на_25_мая_Тимур_Жуков.Data;
using ДЗ_на_25_мая_Тимур_Жуков.Data.Entities;
using ДЗ_на_25_мая_Тимур_Жуков.Models;

namespace ДЗ_на_25_мая_Тимур_Жуков.Repositories
{
    public class AccountRepo : IAccountRepo
    {
        private readonly AppDbContext _dbContext;

        public AccountRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AccountModel?> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Accounts.FindAsync(id);
            return entity == null ? null : MapToModel(entity);
        }

        public async Task<AccountModel?> GetByEmailAsync(string email)
        {
            var entity = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Email == email);
            return entity == null ? null : MapToModel(entity);
        }

        public async Task<List<AccountModel>> GetByRoleAsync(AccountRole role)
        {
            return await _dbContext.Accounts
                .Where(a => a.Role == role)
                .Select(a => new AccountModel
                {
                    Id = a.Id,
                    Email = a.Email,
                    PasswordHash = a.PasswordHash,
                    Role = a.Role
                })
                .ToListAsync();
        }

        public async Task CreateAsync(AccountModel account)
        {
            var entity = new AccountEntity
            {
                Email = account.Email,
                PasswordHash = account.PasswordHash,
                Role = account.Role
            };
            _dbContext.Accounts.Add(entity);
            await _dbContext.SaveChangesAsync();
            account.Id = entity.Id;
        }

        public async Task UpdateAsync(AccountModel account)
        {
            var entity = await _dbContext.Accounts.FindAsync(account.Id);
            if (entity == null) return;

            entity.Email = account.Email;
            entity.PasswordHash = account.PasswordHash;
            entity.Role = account.Role;
            await _dbContext.SaveChangesAsync();
        }

        private static AccountModel MapToModel(AccountEntity entity)
        {
            return new AccountModel
            {
                Id = entity.Id,
                Email = entity.Email,
                PasswordHash = entity.PasswordHash,
                Role = entity.Role
            };
        }
    }
}