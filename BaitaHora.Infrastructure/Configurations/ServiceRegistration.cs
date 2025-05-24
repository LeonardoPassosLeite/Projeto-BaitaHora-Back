using BaitaHora.Application.IRepository;
using BaitaHora.Application.IServices;
using BaitaHora.Application.IServices.Auth;
using BaitaHora.Infrastructure.Data;
using BaitaHora.Infrastructure.Repositories;
using BaitaHora.Infrastructure.Services.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BaitaHora.Infrastructure.Services
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();

            //Service
            services.AddScoped<IAuthService, AuthService>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}