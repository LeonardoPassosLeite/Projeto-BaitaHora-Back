using BaitaHora.Domain.Entities;

namespace BaitaHora.Application.IRepository
{

    public interface IUserRepository
    {
        Task<bool> ExistsByUsernameAsync(string username);
        Task AddAsync(User user);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByIdAsync(Guid id);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<List<User>> ListAsync(); 
    }
}