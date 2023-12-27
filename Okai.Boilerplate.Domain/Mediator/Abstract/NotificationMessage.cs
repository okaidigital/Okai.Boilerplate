namespace Okai.Boilerplate.Domain.Mediator.Abstract
{
    public abstract class NotificationMessage
    {
        public string NotificationType { get; protected set; }
        public DateTime SendAt { get; protected set; }

        protected NotificationMessage()
        {
            NotificationType = GetType().Name;
            SendAt = DateTime.Now;
        }
    }
}
