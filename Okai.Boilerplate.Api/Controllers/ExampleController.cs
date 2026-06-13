using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Okai.Boilerplate.Api.Common;
using Okai.Boilerplate.Application.Commands.Example;
using Okai.Boilerplate.Application.DTOs;
using Okai.Boilerplate.Application.DTOs.Base;
using Okai.Boilerplate.Domain.Mediator;

namespace Okai.Boilerplate.Api.Controllers
{
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
        public ExampleController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(SuccessResponseDto<ExampleState>), 200)]
        public async Task<BaseResponseDto> ExampleCommand([FromBody] ExampleDto request)
        {
            var command = new ExampleCommand();
            var state = await ExecuteAsync<ExampleCommand, ExampleState>(command);

            return new SuccessResponseDto<ExampleState>(System.Net.HttpStatusCode.OK, state);
        }
    }
}
