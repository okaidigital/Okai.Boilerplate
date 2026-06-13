using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Okai.Boilerplate.Infrastructure.Data.RelationalDatabase
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Dev";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

            var preliminaryConfiguration = builder.Build();

            var keyVaultUrl = preliminaryConfiguration["KeyVaultSettings:Uri"];
            if (!Uri.TryCreate(keyVaultUrl, UriKind.Absolute, out var keyVaultUri))
            {
                throw new InvalidOperationException("KeyVaultSettings:Uri must be configured with an absolute URI.");
            }

            var client = new SecretClient(keyVaultUri, new DefaultAzureCredential());

            builder.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());

            var finalConfiguration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var connectionString = finalConfiguration["Your-Project-Database-ConnectionString"] ??
                throw new InvalidOperationException("The database connection string is not configured.");

            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
