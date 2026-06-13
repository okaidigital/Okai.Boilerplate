using Okai.Boilerplate.Application.Configuration;
using Okai.Boilerplate.Domain.Middlewares;

var appBuilder = WebApplication.CreateBuilder(args);

appBuilder.Configuration
    .SetBasePath(appBuilder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{appBuilder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .ConfigureKeyVault();

appBuilder.Services.AddRelationalDatabase(appBuilder.Configuration);
appBuilder.Services.AddMessageBroker(appBuilder.Configuration);
appBuilder.Services.AddApplicationServices();
appBuilder.Services.AddAzureServices(appBuilder.Configuration);
appBuilder.Services.AddMediator();

appBuilder.Services.AddApiConfiguration();
appBuilder.Services.AddEndpointsApiExplorer();
appBuilder.Services.AddCorsConfiguration();
appBuilder.Services.AddSwaggerConfiguration();
appBuilder.Services.AddAuthorizationSettings(appBuilder.Configuration);

var applicationInsightsConnectionString =
    appBuilder.Configuration["ApplicationInsights:ConnectionString"] ??
    Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING");

if (!string.IsNullOrWhiteSpace(applicationInsightsConnectionString))
{
    appBuilder.Services.AddApplicationInsightsTelemetry(options =>
        options.ConnectionString = applicationInsightsConnectionString);
}


var app = appBuilder.Build();

app.UseApiConfiguration(app.Environment);
app.UseCorsConfiguration();
app.UseMiddleware<ExceptionMiddleware>();
app.ConfigureSwagger();
app.UseRouting();
app.UseAuthorizationSettings();
app.MapControllers();
app.Run();
