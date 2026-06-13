using Microsoft.Extensions.DependencyInjection;
using Okai.Boilerplate.Domain.Mediator.Abstract;
using DomainMediator = Okai.Boilerplate.Domain.Mediator.Mediator;

namespace Okai.Boilerplate.Domain.Tests;

[TestClass]
public sealed class MediatorTests
{
    [TestMethod]
    public async Task SendMessage_ThrowsEnglishMessageWhenHandlerIsMissing()
    {
        var mediator = new DomainMediator(new ServiceCollection().BuildServiceProvider());

        var exception = await Assert.ThrowsExactlyAsync<ApplicationException>(
            () => mediator.SendMessage(new TestRequestMessage()));

        Assert.AreEqual(
            "Request message TestRequestMessage does not have a registered handler.",
            exception.Message);
    }

    private sealed class TestRequestMessage : RequestMessage
    {
    }
}
