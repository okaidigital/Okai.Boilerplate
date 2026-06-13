using Microsoft.AspNetCore.Mvc;
using Okai.Boilerplate.Domain.Mediator;
using Okai.Boilerplate.Domain.Mediator.Abstract;

namespace Okai.Boilerplate.Api.Common
{
    public class MainController : ControllerBase
    {
        private readonly IMediator _mediator;

        protected MainController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<TU?> ExecuteAsync<T, TU>(T command)
            where T : RequestMessageWithState<TU>
            where TU : State
        {
            var state = await _mediator
                .SendMessage<T, TU>(command);

            return state;
        }

        protected async Task ExecuteAsync<T>(T command)
            where T : Command
        {
            await _mediator.SendMessage(command);
        }
    }
}
