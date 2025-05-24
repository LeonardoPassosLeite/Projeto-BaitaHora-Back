using BaitaHora.Application.DTOs.Auth.Requests;
using BaitaHora.Domain.Commons;
using BaitaHora.Domain.Entities;

namespace BaitaHora.Application.IServices.Auth
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(RegisterRequest request);
        Task<Result<User>> AuthenticateAsync(LoginRequest request);
    }

}
