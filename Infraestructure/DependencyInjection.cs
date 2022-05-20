using Application.Core.Interface;
using Infraestructure.Persistence;
using Infraestructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(x =>
            {
                x.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IDataContext>(provider => provider.GetRequiredService<DataContext>());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                    .GetBytes(configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IMoveMoneyRepository, MoveMoneyRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            return services;
        }
    }
}
