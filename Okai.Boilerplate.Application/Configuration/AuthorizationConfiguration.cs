using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Okai.Boilerplate.Application.Configuration
{
    public static class AuthorizationConfiguration
    {
        public static void AddAuthorizationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            byte[] key = Encoding.ASCII.GetBytes("ADD YOUR KEY HERE");

            services.AddAuthentication(delegate (AuthenticationOptions o)
            {
                o.DefaultAuthenticateScheme = "Bearer";
                o.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(delegate (JwtBearerOptions o)
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            services.AddAuthorization(delegate (AuthorizationOptions options)
            {
                //ADD YOUR CLAIMS HERE
            });
        }

        public static void UseAuthorizationSettings(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
