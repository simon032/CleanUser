using CleanUser.ApplicationCore.Interfaces;
using CleanUser.Infrastructure.Persistence.Context;
using CleanUser.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanUser.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<UserContext>(options =>
               options.UseSqlServer(defaultConnectionString));

            //services.AddScoped<IUserRepository, ADOUserRepository>();
            services.AddScoped<IUserRepository, EFUserRepository>();

            return services;
        }
    }
}
