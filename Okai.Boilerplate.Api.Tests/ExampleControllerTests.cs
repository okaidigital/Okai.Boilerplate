using Okai.Boilerplate.Api.Controllers;
using Okai.Boilerplate.Application.Commands.Example;
using Okai.Boilerplate.Application.DTOs;
using Okai.Boilerplate.Application.DTOs.Base;
using Okai.Boilerplate.Domain.Mediator;
using Okai.Boilerplate.Domain.Mediator.Abstract;

namespace Okai.Boilerplate.Api.Tests;

[TestClass]
public sealed class ExampleControllerTests
{
    [TestMethod]
    public async Task ExampleCommand_ReturnsSuccessfulResponse()
    {
        var controller = new ExampleController(new StubMediator());

        var response = await controller.ExampleCommand(new ExampleDto());
        var successResponse = (SuccessResponseDto<ExampleState>)response;

        Assert.IsTrue(successResponse.Succeeded);
        Assert.IsNotNull(successResponse.Data);
        Assert.AreEqual("Handled", successResponse.Data.Message);
    }

    private sealed class StubMediator : IMediator
    {
        public Task SendMessage<TRequestMessage>(TRequestMessage requestMessage)
            where TRequestMessage : RequestMessage
        {
            return Task.CompletedTask;
        }

        public Task<TResponseMessage?> SendMessage<TRequestMessage, TResponseMessage>(TRequestMessage requestMessage)
            where TRequestMessage : RequestMessageWithState<TResponseMessage>
            where TResponseMessage : ResponseMessage
        {
            var response = typeof(TResponseMessage) == typeof(ExampleState)
                ? (TResponseMessage)(object)new ExampleState("Handled")
                : null;

            return Task.FromResult(response);
        }

        public Task Notify<TNotificationMessage>(TNotificationMessage notificationMessage)
            where TNotificationMessage : NotificationMessage
        {
            return Task.CompletedTask;
        }
    }
}
