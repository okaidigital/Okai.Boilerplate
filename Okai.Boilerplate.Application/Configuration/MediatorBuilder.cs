using Microsoft.Extensions.DependencyInjection;
using Okai.Boilerplate.Domain.Contracts;
using Okai.Boilerplate.Domain.Mediator;
using System.Collections.Concurrent;

namespace Okai.Boilerplate.Application.Configuration
{
    public class MediatorBuilder : IDisposable
    {
        private readonly Type[] _projectRootAssemblyTypes;
        private readonly ConcurrentDictionary<Type, Type> _interfaceToImplementationMapper;

        public MediatorBuilder(Type classTypeFromProjectRoot)
        {
            _projectRootAssemblyTypes = classTypeFromProjectRoot.Assembly.GetTypes();
            _interfaceToImplementationMapper = new ConcurrentDictionary<Type, Type>();
        }

        public static MediatorBuilder CreateBuilder(Type classTypeFromProjectRoot)
        {
            return new(classTypeFromProjectRoot);
        }

        public MediatorBuilder AddHandlers()
        {
            foreach (var type in _projectRootAssemblyTypes)
            {
                var interfaces = type.GetInterfaces().Where(type =>
                    type.IsGenericType &&
                    (type.GetGenericTypeDefinition() == typeof(IRequestMessageHandler<>) ||
                     type.GetGenericTypeDefinition() == typeof(IRequestMessageWithStateHandler<,>) ||
                     type.GetGenericTypeDefinition() == typeof(INotificationSubscriber<>)));

                foreach (Type interfaceType in interfaces)
                    _interfaceToImplementationMapper.TryAdd(interfaceType, type);
            }

            return this;
        }

        public void Build(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IMediator, Mediator>();

            foreach (var keyValuePair in _interfaceToImplementationMapper)
                serviceCollection.AddScoped(keyValuePair.Key, keyValuePair.Value);

            Dispose();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
