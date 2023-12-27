using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Okai.Boilerplate.Domain.Mediator;
using Okai.Boilerplate.Domain.Mediator.Abstract;

namespace Okai.Boilerplate.API.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class MainController : ControllerBase
    {
        private readonly IMediator _mediator;
        internal readonly IMapper Mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpAccessor"></param>
        /// <param name="mediator"></param>
        /// <param name="mapper"></param>
        public MainController(IHttpContextAccessor httpAccessor, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            Mapper = mapper;
        }

        internal async Task<TU?> ExecuteAsync<T, TU>(T command)
            where T : RequestMessageWithState<TU>
            where TU : State
        {
            var state = await _mediator
                .SendMessage<T, TU>(command);

            return state;
        }

        internal async Task ExecuteAsync<T>(T command)
            where T : Command
        {
            await _mediator.SendMessage(command);
        }
    }
}