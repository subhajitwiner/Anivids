using api.DataConfig;
using api.Interfaces;
using api.Services;
using Microsoft.EntityFrameworkCore;

namespace api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, 
            IConfiguration configuration) 
        {
            services.AddDbContext<DataContext>(Options => {
                Options.UseSqlServer(configuration.GetConnectionString("Default"));
            });
            services.AddScoped<ITokenService, TokenService>();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                });
            });
            services.AddSignalR();
            return services;
        }
    }
}
