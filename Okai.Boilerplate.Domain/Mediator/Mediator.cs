using Microsoft.Extensions.DependencyInjection;
using Okai.Boilerplate.Domain.Contracts;
using Okai.Boilerplate.Domain.Mediator.Abstract;

namespace Okai.Boilerplate.Domain.Mediator
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task SendMessage<TRequestMessage>(TRequestMessage requestMessage) where TRequestMessage : RequestMessage
        {
            if (_serviceProvider.GetService(typeof(IRequestMessageHandler<TRequestMessage>)) is
                IRequestMessageHandler<TRequestMessage> requestMessageHandler)
                await requestMessageHandler.Handle(requestMessage);
            else
                throw new ApplicationException(
                    $"Comando: {requestMessage.GetType().Name} - não possui nenhum responsável");
        }

        public async Task<TResponseMessage?> SendMessage<TRequestMessage, TResponseMessage>(TRequestMessage requestMessage)
            where TRequestMessage : RequestMessageWithState<TResponseMessage>
            where TResponseMessage : ResponseMessage
        {
            if (_serviceProvider.GetService(typeof(IRequestMessageWithStateHandler<TRequestMessage, TResponseMessage>)) is
                IRequestMessageWithStateHandler<TRequestMessage, TResponseMessage> requestMessageHandler)
                return await requestMessageHandler.Handle(requestMessage);

            throw new ApplicationException($"Comando: {requestMessage.GetType().Name} - não possui nenhum responsável");
        }

        public async Task Notify<TNotificationMessage>(TNotificationMessage notificationMessage) where TNotificationMessage : NotificationMessage
        {
            if (_serviceProvider.GetServices(typeof(INotificationSubscriber<TNotificationMessage>)) is
                IEnumerable<INotificationSubscriber<TNotificationMessage>> subscribers)
                foreach (var subscriber in subscribers) await subscriber.Receive(notificationMessage);
        }
    }
}
