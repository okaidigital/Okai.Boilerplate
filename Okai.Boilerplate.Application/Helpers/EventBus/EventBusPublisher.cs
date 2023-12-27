using MassTransit;
using Okai.Boilerplate.Application.Configuration;

namespace Okai.Boilerplate.Application.Helpers.EventBus
{
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

    public interface IEventBusPublisher
    {
        Task Publish<TEvent>(TEvent @event);
        Task PublishBatch<TEvent>(IEnumerable<TEvent>? events) where TEvent : class;
    }
}
