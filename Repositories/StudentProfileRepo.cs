using Microsoft.EntityFrameworkCore;
using ДЗ_на_25_мая_Тимур_Жуков.Data;
using ДЗ_на_25_мая_Тимур_Жуков.Data.Entities;
using ДЗ_на_25_мая_Тимур_Жуков.Models;

namespace ДЗ_на_25_мая_Тимур_Жуков.Repositories
{
    public class StudentProfileRepo : IStudentProfileRepo
    {
        private readonly AppDbContext _dbContext;

        public StudentProfileRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StudentProfileModel?> GetByIdAsync(int id)
        {
            var entity = await _dbContext.StudentProfiles.FindAsync(id);
            return entity == null ? null : MapToModel(entity);
        }

        public async Task<StudentProfileModel?> GetByAccountIdAsync(int accountId)
        {
            var entity = await _dbContext.StudentProfiles
                .FirstOrDefaultAsync(p => p.AccountId == accountId);
            return entity == null ? null : MapToModel(entity);
        }

        public async Task<List<StudentProfileModel>> GetAllAsync()
        {
            return await _dbContext.StudentProfiles
                .Select(p => new StudentProfileModel
                {
                    Id = p.Id,
                    AccountId = p.AccountId,
                    FullName = p.FullName,
                    GroupName = p.GroupName,
                    PhotoUrl = p.PhotoUrl
                })
                .ToListAsync();
        }

        public async Task CreateAsync(StudentProfileModel profile)
        {
            var entity = new StudentProfileEntity
            {
                AccountId = profile.AccountId,
                FullName = profile.FullName,
                GroupName = profile.GroupName,
                PhotoUrl = profile.PhotoUrl
            };
            _dbContext.StudentProfiles.Add(entity);
            await _dbContext.SaveChangesAsync();
            profile.Id = entity.Id;
        }

        private static StudentProfileModel MapToModel(StudentProfileEntity entity)
        {
            return new StudentProfileModel
            {
                Id = entity.Id,
                AccountId = entity.AccountId,
                FullName = entity.FullName,
                GroupName = entity.GroupName,
                PhotoUrl = entity.PhotoUrl
            };
        }
    }
}