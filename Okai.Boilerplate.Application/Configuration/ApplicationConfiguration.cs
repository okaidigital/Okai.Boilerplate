using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Okai.Boilerplate.Application.Settings;
using Okai.Boilerplate.Domain.Contracts.Data.RelationalDatabase;
using Okai.Boilerplate.Infrastructure.Data.RelationalDatabase;
using System.Reflection;

namespace Okai.Boilerplate.Application.Configuration
{
    public static class ApplicationConfiguration
    {
        public static void AddRelationalDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IRepositoryManager, RepositoryManager>();

            var databaseConnectionString = configuration["Your-Project-Database-ConnectionString"] ??
                throw new InvalidOperationException("The database connection string is not configured.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(databaseConnectionString), ServiceLifetime.Transient);
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterServicesWithAttributes(assembly);

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static void AddMediator(this IServiceCollection services)
        {
            MediatorBuilder
                .CreateBuilder(typeof(ApplicationConfiguration))
                .AddHandlers()
                .Build(services);
        }

        /// <summary>
        ///     Add listener/consumers for message broker
        /// </summary>
        /// <param name="services">Extension method</param>
        /// <param name="configuration">Interface for fetching appsettings.json information</param>
        public static void AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(msCfg =>
            {
                msCfg.AddServiceBusMessageScheduler();

                //msCfg.AddConsumer<YourQueueService>();

                var settings =
                    configuration.GetSection("EventBusSettings").Get<EventBusSettings>() ??
                    throw new ApplicationException(
                        $"AppSettings file doesn't contain the key {nameof(EventBusSettings)}");

                var hostAddress = configuration["EventBus-HostAddress"];

                msCfg.UsingAzureServiceBus((ctx, cfg) =>
                {

                    cfg.Host(hostAddress);

                    //cfg.ReceiveEndpoint(settings.your_queue, config =>
                    // config.ConfigureConsumer<YourQueueService>(ctx));

                    cfg.UseServiceBusMessageScheduler();
                    cfg.ConfigureEndpoints(ctx);
                });
            });
        }

        public static void AddAzureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Your-Project-FileStorage-ConnectionString"] ??
                throw new InvalidOperationException("The Azure Blob Storage connection string is not configured.");

            services.AddAzureClients(azureClientsBuilder =>
            {
                azureClientsBuilder.AddBlobServiceClient(connectionString);
            });
        }

        public static void ConfigureKeyVault(this IConfigurationBuilder configurationBuilder)
        {
            var settings = configurationBuilder.Build();

            var keyVaultUrl = settings["KeyVaultSettings:Uri"];
            if (string.IsNullOrWhiteSpace(keyVaultUrl) ||
                keyVaultUrl.Equals("YOUR_VAULT_URL", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (!Uri.TryCreate(keyVaultUrl, UriKind.Absolute, out var keyVaultUri))
            {
                throw new InvalidOperationException("KeyVaultSettings:Uri must be an absolute URI.");
            }

            var client = new SecretClient(keyVaultUri, new DefaultAzureCredential());

            configurationBuilder.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
        }
    }
}
