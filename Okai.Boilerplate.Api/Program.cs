using Okai.Boilerplate.Application.Configuration;
using Okai.Boilerplate.Domain.Middlewares;


var appBuilder = WebApplication.CreateBuilder(args);

appBuilder.Host.ConfigureAppConfiguration((context, config) => {
    config.ConfigureKeyVault();
});

appBuilder.Configuration
    .SetBasePath(appBuilder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{appBuilder.Environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

appBuilder.Services.AddRelationalDatabase(appBuilder.Configuration);
appBuilder.Services.AddMessageBroker(appBuilder.Configuration);
appBuilder.Services.AddApplicationServices();
appBuilder.Services.AddAzureServices(appBuilder.Configuration);
appBuilder.Services.AddAutoMapper();
appBuilder.Services.AddMediator();

appBuilder.Services.AddApiConfiguration();
appBuilder.Services.AddEndpointsApiExplorer();
appBuilder.Services.AddCorsConfiguration();
appBuilder.Services.AddSwaggerConfiguration();
appBuilder.Services.AddAuthorizationSettings(appBuilder.Configuration);
appBuilder.Services.AddApplicationInsightsTelemetry(Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING"));


var app = appBuilder.Build();

app.UseApiConfiguration(app.Environment);
app.UseCorsConfiguration();
app.UseMiddleware<ExceptionMiddleware>();
app.ConfigureSwagger();
app.UseRouting();
app.UseAuthorizationSettings();
app.MapControllers();
app.Run();