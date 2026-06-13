# Okai.Boilerplate

Okai.Boilerplate is a .NET 8 application template for building API projects with a layered architecture, SQL Server persistence, Azure integrations, JWT authentication, and Azure DevOps pipeline scaffolding.

## What Is Included

- ASP.NET Core API project.
- Application, Domain, Infrastructure, and API layers.
- SQL Server persistence with Entity Framework Core.
- Azure Key Vault configuration support.
- Azure Blob Storage client registration.
- Azure Service Bus integration through MassTransit.
- JWT bearer authentication setup.
- API versioning, CORS, Swagger, and centralized exception middleware.
- Attribute-based service registration for scoped, singleton, and transient services.
- Azure DevOps pipeline template for restore, test, migration, build, publish, and artifact upload.

## Requirements

- .NET 8 SDK.
- SQL Server or Azure SQL Database.
- Azure Key Vault when running with external secrets.
- Azure Service Bus and Azure Blob Storage when those integrations are enabled.

## Solution Structure

- `Okai.Boilerplate.Api`: API host, controllers, middleware pipeline, and environment settings.
- `Okai.Boilerplate.Application`: application services, commands, DTOs, configuration, and integration helpers.
- `Okai.Boilerplate.Domain`: domain entities, mediator contracts, validation, exceptions, and shared abstractions.
- `Okai.Boilerplate.Infrastructure`: EF Core `DbContext`, repositories, design-time migration support, and seed configuration.
- `*.Tests`: test projects for each layer.

## Configuration

Environment-specific settings are loaded from:

- `appsettings.json`
- `appsettings.dev.json`
- `appsettings.staging.json`
- `appsettings.main.json`
- environment variables
- Azure Key Vault, when `KeyVaultSettings:Uri` is configured with a real absolute URI

Replace these placeholder secret names with values that match your project:

- `Your-Project-Database-ConnectionString`
- `Your-Project-FileStorage-ConnectionString`
- `EventBus-HostAddress`

`YOUR_VAULT_URL` is treated as a placeholder and is skipped locally, so the template can build before real Azure resources are configured.

## Attribute-Based Service Registration

Annotate service classes with one of these attributes to register them automatically in the .NET dependency injection container:

- `[ScopedService]`
- `[SingletonService]`
- `[TransientService]`

Example:

```csharp
[ScopedService]
public class EventBusPublisher : IEventBusPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public EventBusPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Publish<TEvent>(TEvent @event)
    {
        if (@event is null)
        {
            return;
        }

        await _publishEndpoint.Publish(@event);
    }
}
```

## Database Migrations

Install the EF Core CLI:

```bash
dotnet tool install --global dotnet-ef --version 8.0.28
```

Set the environment before creating or applying migrations.

macOS/Linux:

```bash
export ASPNETCORE_ENVIRONMENT=Dev
```

Windows PowerShell:

```powershell
$env:ASPNETCORE_ENVIRONMENT = "Dev"
```

Create a migration:

```bash
dotnet ef migrations add YourMigrationName \
  --project Okai.Boilerplate.Infrastructure/Okai.Boilerplate.Infrastructure.csproj \
  --startup-project Okai.Boilerplate.Api/Okai.Boilerplate.Api.csproj
```

Apply migrations:

```bash
dotnet ef database update \
  --project Okai.Boilerplate.Infrastructure/Okai.Boilerplate.Infrastructure.csproj \
  --startup-project Okai.Boilerplate.Api/Okai.Boilerplate.Api.csproj
```

## Build and Test

```bash
dotnet restore
dotnet build
dotnet test
```

## Creating a Project From This Template

1. Clone this repository.
2. Copy the project folders into a new repository, excluding the `.git` folder.
3. Rename namespaces, assemblies, projects, and configuration keys from `Okai.Boilerplate` to your project name.
4. Configure Azure Key Vault, SQL Server, Blob Storage, Service Bus, and JWT settings.
5. Grant the deployed application identity access to the Azure Key Vault secrets it needs.
6. Update `azure-pipelines.yml` with your Azure subscription, feed, branch strategy, and deployment conventions.

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.
