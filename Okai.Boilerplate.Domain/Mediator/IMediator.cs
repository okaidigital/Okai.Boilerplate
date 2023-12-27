using Okai.Boilerplate.Domain.Mediator.Abstract;

namespace Okai.Boilerplate.Domain.Mediator
{
    public interface IMediator
    {
        Task SendMessage<TRequestMessage>(TRequestMessage requestMessage) where TRequestMessage : RequestMessage;

        Task<TResponseMessage?> SendMessage<TRequestMessage, TResponseMessage>(TRequestMessage requestMessage)
            where TResponseMessage : ResponseMessage
            where TRequestMessage : RequestMessageWithState<TResponseMessage>;

        Task Notify<TNotificationMessage>(TNotificationMessage notificationMessage) where TNotificationMessage : NotificationMessage;
    }
}
