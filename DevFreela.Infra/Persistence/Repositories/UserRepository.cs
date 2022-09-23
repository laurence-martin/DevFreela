using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infra.Persistense;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infra.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        public UserRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Create(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();    
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(p => p.Email == email && p.Password == password);
        }
    }
}
