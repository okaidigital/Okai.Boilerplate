## What is a boilerplate?
In the context of this project, a [boilerplate is a scaffolding](https://aws.amazon.com/pt/what-is/boilerplate-code/), meaning it's a base code project shared that you can use to create new applications with few changes.

## .NET Version
.NET 8

## Dependencies and Features

The project is already set up and with all packages downloaded for:

- **JWT Authentication**  
Uses JSON Web Tokens to manage user authentication, providing a secure and efficient method for session and identity management.
- **Azure Key Vault**  
Stores secrets, such as API keys and certificates, securely in the cloud, allowing for centralized and secure management of sensitive information.
- **SQL Server**  
Relational database.
- **Entity Framework**  
An ORM that simplifies database access and management.
- **CQRS with Mediator**  
Implements the Command Query Responsibility Segregation (CQRS) pattern with the use of the Mediator library, separating read and write logic to increase code efficiency and clarity.
- **Azure Storage**  
Cloud storage solution for handling large volumes of unstructured data.
- **Azure Event Bus**  
A messaging integration service to facilitate asynchronous communication and event-driven architecture.
- **DbContext in Design-Time Build**  
Allows the Entity Framework configuration to use secrets stored in Azure Key Vault during migration generation and execution.
- **Separate Dev, Homolog, and Prod Environments**  
Specific configurations for each environment are defined in appSettings and launchSettings files, facilitating the management of environment variables and settings by environment.
- **Base Yaml for Azure Pipeline**  
An initial template for setting up CI/CD pipelines in Azure DevOps, automating the build, test, and deploy process.

## Package Versions
- **Entity Framework (Core, Design, Abstractions, SqlServer) - 8.0.3**  
Data modeling, querying, and persistence.
- **Mass Transit - 8.1.0**  
A framework for building distributed applications using messaging.
- **Azure Identity - 1.10.4**  
Facilitates authentication and authorization in Azure services.
- **Azure Security Key Vault - 4.5.0**  
Enables secure access to secrets, keys, and certificates stored in Azure Key Vault.

## Attribute-Based Service Registration  
This project offers a simplified and convention-based approach to register services in the .NET integrated dependency injection container using attributes.
By using the AttributeServiceExtension, you can annotate your service classes with [ScopedService], [SingletonService], or [TransientService] attributes to automatically register them with the corresponding lifetime without needing to manually write registration code.

Features:

- Attribute-Based Service Registration: Easily manage your service's lifetime scope.
- Automatic Registration: Services are automatically registered in the IServiceCollection provided by .NET based on the specified attributes.
- Interface Support: If your service implements interfaces, it registers the service by its interface to follow best dependency injection practices.
- Custom Assembly Scanning: Register services from a specified assembly flexibly.
  
How To Use:   
Mark your service classes with the provided attributes according to the desired lifetime:
- [ScopedService] for scoped lifetime.
- [SingletonService] for singleton lifetime.
- [TransientService] for transient lifetime.
  
Example:

```
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
                return;

            await _publishEndpoint.Publish(@event);
        }

        public async Task PublishBatch<TEvent>(IEnumerable<TEvent>? events) where TEvent : class
        {
            if (events is null) return;

            await _publishEndpoint.PublishBatch(events);
        }
    }
```

## azure-pipelines.yml
The project is pre-configured with a .yml file for the Azure pipeline, which contains:

- Commit and PR triggers for dev, homolog, and main branches.
- Task to download the .NET SDK.
- Task to authenticate and download Nuget packages.
- Task to download Entity Framework.
- Task to execute migrations on the database.
- Task to build, publish, and archive the application.

## Migrations
The project is configured with DbContext in Design-Time Build to fetch the connection string from the vault when creating/executing a migration.

How to create a migration?
1. Open a terminal in the Infrastructure project.
2. Execute the command `$env:ASPNETCORE_ENVIRONMENT = "Dev"` to set which environment you are creating the migration for.
3. Execute the command `dotnet ef migrations add YourMigrationName -s ../Path.To.Project.Startup/` to create the migration.

How to execute a migration?
1. Open a terminal at the root of the solution.
2. Execute the command to set the environment variable: `$env:ASPNETCORE_ENVIRONMENT = "Dev"`
3. Execute the command to update the database: `dotnet ef database update --project Path.To.Your.Project.Infrastructure/Your.Project.Infrastructure.csproj --startup-project Path.To.Your.Project.Startup/Your.Project.Startup.csproj`

## How to create a project using the boilerplate?
1. Clone the project to your machine.
2. Copy the folders, except the hidden .git folder.
3. You can now change the namespaces from Boilerplate to your project's name. (A tip is to press CTRL+SHIFT+F and go to "Replace in Files" for quicker changes).
4. Remember to adjust the secrets stored in Azure Key Vault according to your project's needs. Throughout the configuration or update of the code, you will find comments indicating where these modifications are necessary. Search the code for the "//Change here" markers to locate these specific points.
5. Remember to allow your Azure web app to access the Azure Key Vault (if you are using Azure).

## Open Source
This project is open source, feel free to open a pull request and/or issue :).

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
