using BaitaHora.Domain.Enums;

namespace BaitaHora.Application.DTOs.Auth.Requests
{
    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Client;
    }
}
