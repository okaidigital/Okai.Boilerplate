using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Okai.Boilerplate.Application.Configuration
{
    public static class CorsConfiguration
    {
        private const string Policy = "Okai.Boilerplate";

        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(Policy, policy =>
                {
                    policy
#if DEBUG
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowCredentials()
#endif
#if !DEBUG
                    .AllowAnyOrigin()
#endif
                    .WithMethods("GET", "POST", "PATCH", "PUT", "OPTIONS");
                });
            });
        }

        public static void UseCorsConfiguration(this IApplicationBuilder app) => app.UseCors(Policy);
    }
}