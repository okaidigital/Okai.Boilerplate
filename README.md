# Português

## O que é boilerplate?
No contexto deste projeto, o [boilerplate é um scaffolding](https://aws.amazon.com/pt/what-is/boilerplate-code/), ou seja, um projeto base de código compartilhado que você pode usar para criar novas aplicações fazendo poucas alterações.

## Versão do .NET
.NET 8

## Dependências e Funcionalidades

O projeto já está configurado e com todos os packages baixados para:

- **Autenticação com token JWT**  
   Utiliza JSON Web Tokens para gerenciar a autenticação de usuários, proporcionando um método seguro e eficiente para a gestão de sessões e identidades.
- **Azure Key Vault**  
   Armazena segredos, como chaves de API e certificados, de forma segura na nuvem, permitindo um gerenciamento centralizado e seguro de informações sensíveis.
- **SQL Server**  
   Banco de dados relacional.
- **Entity Framework**  
   Um ORM que simplifica o acesso e a gestão do banco de dados.
- **CQRS com Mediator**  
   Implementa o padrão Command Query Responsibility Segregation (CQRS) com o uso da biblioteca Mediator, separando a lógica de leitura e escrita para aumentar a eficiência e a clareza do código.
- **Azure Storage**  
  Solução de armazenamento na nuvem para lidar com grandes volumes de dados não estruturados.
- **Azure Event Bus**  
   Um serviço de integração de mensagens para facilitar a comunicação assíncrona e a arquitetura orientada a eventos.
- **DbContext em Design-Time Build**  
   Permite a configuração do Entity Framework para utilizar segredos armazenados no Azure Key Vault durante a geração e execução de migrations.
- **Ambientes de Dev, Homolog e Prod separados**  
   Configurações específicas para cada ambiente são definidas em arquivos appSettings e launchSettings, facilitando a gestão de variáveis de ambiente e configurações por ambiente.
- **Yaml base para pipeline do Azure**  
   Um template inicial para a configuração de pipelines de CI/CD no Azure DevOps, automatizando o processo de build, teste e deploy da aplicação.

## Versões dos pacotes
- Entity Framework (Core, Design, Abstractions, SqlServer) - 8.0.3 Modelagem, consulta e persistência de dados.
- Mass Transit - 8.1.0: Um framework para construir aplicações distribuídas usando mensageria.
- Azure Identity - 1.10.4: Facilita a autenticação e autorização em serviços do Azure.
- Azure Security Key Vault - 4.5.0: Permite o acesso seguro a segredos, chaves e certificados armazenados no Azure Key Vault.

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
- Task para baixar o .NET SDK.
- Task para autenticar e baixar pacotes do Nuget.
- Task para baixar o Entity Framework.
- Task para executar as migrations no banco de dados.
- Task para build, publish e archive da aplicação.

## Migrations
O projeto já está configurado com DbContext em Design-Time Build para buscar a connection string do vault ao criar/executar uma migration.

Como criar uma migration?
1. Abrir um terminal no projeto Infrastructure.
2. Execute o comando `$env:ASPNETCORE_ENVIRONMENT = "Dev` para definir qual ambiente você está criando a migration.
3. Execute o comando `dotnet ef migrations add nomeDaSuaMigration -s ../Caminho.Do.Projeto.Startup/` para criar a migration.

Como executar a migration?
1. Abrir um terminal na raíz da solution.
2. Execute o comando para setar a variável de ambiente: `$env:ASPNETCORE_ENVIRONMENT = "Dev"`
3. Execute o comando para dar update no banco: `dotnet ef database update --project Pasta.Do.Seu.Projeto.Infrastructure/Seu.Projeto.Infrastructure.csproj --startup-project Pasta.Do.Seu.Projeto.Startup/Seu.Projeto.Startup.csproj`

## Como criar um projeto usando o boilerplate?
1. Clone o projeto para a sua máquina.
2. Copie as pastas, menos a pasta oculta .git.
3. Agora você pode alterar os namespaces de Boilerplate para o nome do seu projeto. (Uma dica é apertar CTRL+SHIFT+F e ir em "Substituir nos Arquivos", assim fica mais rápido de alterar).
4. Lembre-se de ajustar os segredos armazenados no Azure Key Vault de acordo com as necessidades do seu projeto. Durante a configuração ou atualização do código, você encontrará comentários indicando onde essas modificações são necessárias. Procure no código as marcações "//Altere aqui" para localizar exatamente esses pontos.
5. Lembre-se de permitir que o seu web app do Azure possa acessar o Azure Key Vault (caso você esteja usando o Azure).

## Open Source
Este projeto é open source, fique a vontade para abrir um pull request e/ou issue :).

## Licença
Este projeto está licenciado sob a Licença MIT - veja o arquivo [LICENSE](LICENSE) para detalhes.
