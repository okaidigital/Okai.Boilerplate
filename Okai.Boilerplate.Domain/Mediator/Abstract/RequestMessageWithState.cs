namespace Okai.Boilerplate.Domain.Mediator.Abstract
{
    public abstract class RequestMessageWithState<TResponseMessage> : RequestMessage where TResponseMessage : ResponseMessage { }
}
