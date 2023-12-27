using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Okai.Boilerplate.API.Common;
using Okai.Boilerplate.Application.Commands.Example;
using Okai.Boilerplate.Application.DTOs;
using Okai.Boilerplate.Application.DTOs.Base;
using Okai.Boilerplate.Domain.Mediator;

namespace Okai.Boilerplate.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
#if DEBUG
    [AllowAnonymous]
    #endif
    #if !DEBUG
    [Authorize]
    #endif

    [ApiController]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class ExampleController : MainController
    {
        private readonly ILogger<ExampleController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpAccessor"></param>
        /// <param name="mediator"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public ExampleController(IHttpContextAccessor httpAccessor,
            IMediator mediator,
            IMapper mapper,
            ILogger<ExampleController> logger) : base(httpAccessor,
            mediator,
            mapper)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(SuccessResponseDto<ExampleState>), 200)]
        public async Task<BaseResponseDto> ExampleCommand([FromBody] ExampleDto request)
        {
            var command = Mapper.Map<ExampleDto, ExampleCommand>(request);
            var state = await ExecuteAsync<ExampleCommand, ExampleState>(command);

            return new SuccessResponseDto<ExampleState>(System.Net.HttpStatusCode.OK, state);
        }
    }
}