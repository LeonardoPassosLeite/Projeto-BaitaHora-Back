using BaitaHora.Application.DTOs.Auth.Requests;
using BaitaHora.Application.IRepository;
using BaitaHora.Application.IServices.Auth;
using BaitaHora.Domain.Commons;
using BaitaHora.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BaitaHora.Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository, ILogger<AuthService> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result> RegisterAsync(RegisterRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                    return Result.Failure("Usuário e senha são obrigatórios.");

                var exists = await _userRepository.ExistsByUsernameAsync(request.Username);
                if (exists)
                    return Result.Failure("Nome de usuário já está em uso.");

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                var user = new User(request.Username, passwordHash, request.Role);

                await _userRepository.AddAsync(user);

                _logger.LogInformation("Usuário registrado: {Username}", request.Username);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar usuário.");
                return Result.Failure("Erro interno ao registrar usuário.");
            }
        }

        public async Task<Result<User>> AuthenticateAsync(LoginRequest request)
        {
            try
            {
                var user = await _userRepository.GetByUsernameAsync(request.Username);

                if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                    return Result<User>.Failure("Usuário ou senha inválidos.");

                _logger.LogInformation("Usuário autenticado: {Username}", request.Username);
                return Result<User>.Success(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao autenticar usuário.");
                return Result<User>.Failure("Erro interno ao autenticar.");
            }
        }
    }
}
