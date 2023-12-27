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

            var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

            builder.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());

            var finalConfiguration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            //Change here
            optionsBuilder.UseSqlServer(finalConfiguration["Your-Project-Database-ConnectionString"]);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
