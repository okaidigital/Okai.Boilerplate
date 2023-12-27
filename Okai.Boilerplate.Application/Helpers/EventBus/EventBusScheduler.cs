using MassTransit;
using Okai.Boilerplate.Application.Configuration;

namespace Okai.Boilerplate.Application.Helpers.EventBus
{
    [ScopedService]
    public class EventBusScheduler : IEventBusScheduler
    {
        private readonly IMessageScheduler _messageScheduler;

        public EventBusScheduler(IMessageScheduler messageScheduler)
        {
            _messageScheduler = messageScheduler;
        }

        public async Task Schedule<TEvent>(DateTime time, TEvent @event)
        {
            if (@event is null) return;

            await _messageScheduler.SchedulePublish(time, @event);
        }

        public async Task ScheduleBatch<TEvent>(DateTime time, int intervalInMilliseconds, IEnumerable<TEvent>? events) where TEvent : class
        {
            if (events is null) return;

            foreach (var @event in events)
            {
                await _messageScheduler.SchedulePublish(time, @event);
                time = time.AddMilliseconds(intervalInMilliseconds);
            }
        }
    }

    public interface IEventBusScheduler
    {
        Task Schedule<TEvent>(DateTime time, TEvent @event);
        Task ScheduleBatch<TEvent>(DateTime time, int intervalInMilliseconds, IEnumerable<TEvent> @event) where TEvent : class;
    }
}
