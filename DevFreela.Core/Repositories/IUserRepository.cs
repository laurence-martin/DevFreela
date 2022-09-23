using DevFreela.Core.Entities;
using System.Diagnostics;

namespace DevFreela.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task Create(User user);
        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
    }
}
