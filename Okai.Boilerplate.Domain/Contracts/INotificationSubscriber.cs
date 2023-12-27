using Okai.Boilerplate.Domain.Mediator.Abstract;

namespace Okai.Boilerplate.Domain.Contracts
{
    public interface INotificationSubscriber<in TNotificationMessage> where TNotificationMessage : NotificationMessage
    {
        Task Receive(TNotificationMessage notificationMessage);
    }
}
