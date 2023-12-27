## .NET 6.0

## Dependencies and Features

### English

The project is already set up and all packages downloaded for:

- JWT authentication.
- Azure Key Vault.
- Azure SQL Server.
- Entity Framework.
- CQRS with Mediator.
- Azure Storage.
- Azure Event Bus.
- DbContext in Design-Time Build to run migrations with secrets in the vault.
- Separate Dev, Homolog, and Prod environments in appSettings and launchSettings.
- Base Yaml for Azure pipeline.

## Attribute-Based Service Registration
This project provides a simplified and convention-based approach to register services into the .NET built-in dependency injection container using attributes.
By using the AttributeServiceExtension, you can annotate your service classes with [ScopedService], [SingletonService], or [TransientService] attributes to automatically register them with the corresponding lifetime without manually writing the registration code.

Features:
  - Attribute-Based Service Registration: Easily manage your service's lifetime scope.
  - Automatic Registration: Services are automatically registered into the IServiceCollection provided by .NET based on the specified attributes.
  - Interface Support: If your service implements interfaces, it registers the service by its interface to follow the best practices of dependency injection.
  - Custom Assembly Scanning: Flexibly scan and register services from a specified assembly.

How to Use:

Annotate Your Services: Mark your service classes with the provided attributes according to the desired lifetime:
  - [ScopedService] for scoped lifetime.
  - [SingletonService] for singleton lifetime.
  - [TransientService] for transient lifetime.

Ex:

![image](https://github.com/okaidigital/Okai.Boilerplate/assets/155011243/f92ab8bd-cc41-42e8-baa2-66d6951580f8)


### Português

## Dependências e Funcionalidades

O projeto já está configurado e com todos os packages baixados para:

- Autenticação com token JWT.
- Azure Key Vault.
- Azure SQL Server.
- Entity Framework.
- CQRS com Mediator.
- Azure Storage.
- Azure Event Bus.
- DbContext em Design-Time Build para poder executar migrations com os segredos no vault.
- Ambientes de Dev, Homolog e Prod separados no appSettings e no launchSettings.
- Yaml base para pipeline do azure.


## Registro de Serviços Baseado em Atributos
Este projeto oferece uma abordagem simplificada e baseada em convenções para registrar serviços no contêiner de injeção de dependência integrado do .NET usando atributos.
Ao utilizar a AttributeServiceExtension, você pode anotar suas classes de serviço com os atributos [ScopedService], [SingletonService] ou [TransientService] para registrá-los automaticamente com o tempo de vida correspondente sem precisar escrever manualmente o código de registro.

Funcionalidades:
  - Registro de Serviço Baseado em Atributos: Gerencie facilmente o escopo do tempo de vida do seu serviço.
  - Registro Automático: Os serviços são automaticamente registrados no IServiceCollection fornecido pelo .NET com base nos atributos especificados.
  - Suporte a Interfaces: Se o seu serviço implementa interfaces, ele registra o serviço pela sua interface para seguir as melhores práticas de injeção de dependência.
  - Varredura de Assembly Personalizada: Registre serviços de um assembly especificado de forma flexível.

Como Usar:
    Anote Seus Serviços: Marque suas classes de serviço com os atributos fornecidos de acordo com o tempo de vida desejado:
   - [ScopedService] para tempo de vida de escopo.
   - [SingletonService] para tempo de vida único.
   - [TransientService] para tempo de vida transitório.

Ex:

![image](https://github.com/okaidigital/Okai.Boilerplate/assets/155011243/f92ab8bd-cc41-42e8-baa2-66d6951580f8)

