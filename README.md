# Português

## O que é boilerplate?
No contexto deste projeto, o boilerplate é um scaffolding, ou seja, um projeto base de código compartilhado que você pode usar para criar novas aplicações fazendo poucas alterações.

Para mais informações: [Artigo da Amazon](https://aws.amazon.com/pt/what-is/boilerplate-code/)

## Versão do .NET
.NET 8

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
  
## Versões dos pacotes
- Entity Framework (Core, Design, Abstractions, SqlServer) - 8.0.3
- Mass Transit - 8.1.0
- Azure Identity - 1.10.4
- Azure Security Key Vault - 4.5.0

## Registro de Serviços Baseado em Atributos
Este projeto oferece uma abordagem simplificada e baseada em convenções para registrar serviços no contêiner de injeção de dependência integrado do .NET usando atributos.
Ao utilizar a AttributeServiceExtension, você pode anotar suas classes de serviço com os atributos [ScopedService], [SingletonService] ou [TransientService] para registrá-los automaticamente com o tempo de vida correspondente sem precisar escrever manualmente o código de registro.

Funcionalidades:
  - Registro de Serviço Baseado em Atributos: Gerencie facilmente o escopo do tempo de vida do seu serviço.
  - Registro Automático: Os serviços são automaticamente registrados no IServiceCollection fornecido pelo .NET com base nos atributos especificados.
  - Suporte a Interfaces: Se o seu serviço implementa interfaces, ele registra o serviço pela sua interface para seguir as melhores práticas de injeção de dependência.
  - Varredura de Assembly Personalizada: Registre serviços de um assembly especificado de forma flexível.

Como Usar: Marque suas classes de serviço com os atributos fornecidos de acordo com o tempo de vida desejado:
   - [ScopedService] para tempo de vida de escopo.
   - [SingletonService] para tempo de vida único.
   - [TransientService] para tempo de vida transitório.

Ex:

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
O projeto já está pré-configurado com um arquivo .yml com o pipeline do Azure, esse arquivo contém:
- Triggers de commit e PR nas branches de dev, homolog e main.
- Task para baixar .NET sdk.
- Task para autenticar e baixar pacotes do Nuget.
- Task para baixar o Entity Framework.
- Task para executar as migrations no banco de dados.
- Task para build, publish e archive da aplicação.

## Migrations
O projeto já está configurado com DbContext em Design-Time Build para buscar a connection string do vault ao criar/executar uma migration.

Como criar uma migration?
1. Abrir um terminal no projeto Infrastructure.
2. Rodar o comando `$env:ASPNETCORE_ENVIRONMENT = "Dev` para definir qual ambiente você está criando a migration.
3. Rodar o comando `dotnet ef migrations add nomeDaSuaMigration -s ../Caminho.Do.Projeto.Startup/` para criar a migration.

Como executar a migration?
1. Abrir um terminal na raíz da solution.
2. Execute o comando para setar a variável de ambiente: `$env:ASPNETCORE_ENVIRONMENT = "Dev"`
3. Execute o comando para dar update no banco: `dotnet ef database update --project Pasta.Do.Seu.Projeto.Infrastructure/Seu.Projeto.Infrastructure.csproj --startup-project Pasta.Do.Seu.Projeto.Startup/Seu.Projeto.Startup.csproj`

## Como criar um projeto usando o boilerplate?
1. Clone o projeto para a sua máquina.
2. Copie as pastas, menos a pasta oculta .git.
3. Agora você pode alterar os namespaces de Boilerplate para o nome do seu projeto. (Uma dica é apertar CTRL+SHIFT+F e ir em "Substituir nos Arquivos", assim fica mais rápido de alterar).
4. Lembre-se de criar e/ou alterar os seus segredos no Azure Key Vault, tem uns comentários no código do boilerplate com as partes em que você deve alterar, você pode encontrar eles buscando por "//Altere aqui".
5. Lembre-se de permitir que o seu web app do Azure possa acessar o Azure Key Vault (caso você esteja usando o Azure).

## Open Source
Este projeto é open source, sinta-se livre para abrir um pull request e/ou issue :)
