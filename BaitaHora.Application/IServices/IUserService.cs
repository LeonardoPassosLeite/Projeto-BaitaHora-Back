using BaitaHora.Domain.Commons;
using BaitaHora.Domain.Entities;

namespace BaitaHora.Application.IServices
{
    public interface IUserService
    {
        Task<Result> UpdateProfileImageAsync(Guid userId, string imageUrl);
        Task<Result> DeleteAsync(Guid id);
        Task<User?> GetByIdAsync(Guid id);
        Task<List<User>> ListAllAsync();
    }
}