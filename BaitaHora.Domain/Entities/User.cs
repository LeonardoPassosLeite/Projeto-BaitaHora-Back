using BaitaHora.Domain.Enums;

namespace BaitaHora.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Username { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string? ProfileImageUrl { get; private set; }
        public UserRole Role { get; private set; } = UserRole.Client;

        public string? FullName { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? CPF { get; private set; }


        protected User() { }

        public User(string username, string passwordHash, UserRole role = UserRole.Client)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username é obrigatório.");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Senha é obrigatória.");

            Username = username;
            PasswordHash = passwordHash;
            Role = role;
        }

        public void SetProfileImage(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                throw new ArgumentException("URL da imagem é obrigatória.");

            ProfileImageUrl = imageUrl;
        }
    }
}