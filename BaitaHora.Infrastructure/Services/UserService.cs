using BaitaHora.Application.IRepository;
using BaitaHora.Application.IServices;
using BaitaHora.Domain.Commons;
using BaitaHora.Domain.Entities;

namespace BaitaHora.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Result> UpdateProfileImageAsync(Guid userId, string imageUrl)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user is null)
                    return Result.Failure("Usuário não encontrado.");

                user.SetProfileImage(imageUrl);
                await _userRepository.UpdateAsync(user);

                return Result.Success();
            }
            catch
            {
                return Result.Failure("Erro ao atualizar imagem de perfil.");
            }
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user is null)
                    return Result.Failure("Usuário não encontrado.");

                await _userRepository.DeleteAsync(user);
                return Result.Success();
            }
            catch
            {
                return Result.Failure("Erro ao deletar usuário.");
            }
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<List<User>> ListAllAsync()
        {
            return await _userRepository.ListAsync();
        }
    }
}
