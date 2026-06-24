using AuthSefviceApi1.Data;
using AuthSefviceApi1.Data.Repositorys;
using Microsoft.EntityFrameworkCore;

namespace AuthSefviceApi1.Services.Extensions
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddAuthServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<AccountRepository>();
            services.AddScoped<JwtService>();
            services.AddScoped<AccountService>();

            return services;
        }
    }
}
