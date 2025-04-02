using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;

namespace BenimSalonumAPI.DataAccess.Services
{
    public static class JwtAuthenticationService
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration["JwtSettings:Secret"];
            if (string.IsNullOrWhiteSpace(secretKey))
            {
                throw new ArgumentNullException(nameof(secretKey), "JWT Secret Key ayarlanmamış!");
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "BenimSalonumAuth";
                options.DefaultChallengeScheme = "BenimSalonumAuth";
            })
            .AddScheme<BenimSalonumAuthOptions, BenimSalonumAuthHandler>("BenimSalonumAuth", options =>
            {
                options.SecretKey = secretKey;
            });

            Console.WriteLine("Özel JWT authentication başarıyla yapılandırıldı");
        }
    }
}
