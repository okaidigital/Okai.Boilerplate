using Okai.Boilerplate.Domain.Entities;
using Okai.Boilerplate.Domain.Entities.Abstract;

namespace Okai.Boilerplate.Domain.Tests;

[TestClass]
public sealed class EntityTests
{
    [TestMethod]
    public void Constructor_AssignsGlobalIdAndInitializesEvents()
    {
        var entity = new Example();

        Assert.AreNotEqual(Guid.Empty, entity.GlobalId);
        Assert.IsEmpty(entity.Events);
    }

    [TestMethod]
    public void Events_CanBeAddedAndCleared()
    {
        var entity = new Example();
        var @event = new TestEvent();

        entity.AddEvent(@event);
        entity.CleanEvents();

        Assert.IsEmpty(entity.Events);
    }

    private sealed class TestEvent : Event
    {
    }
}
