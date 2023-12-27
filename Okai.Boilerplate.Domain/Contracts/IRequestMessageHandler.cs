using Okai.Boilerplate.Domain.Mediator.Abstract;

namespace Okai.Boilerplate.Domain.Contracts
{
    public interface IRequestMessageHandler<in TRequestMessage> where TRequestMessage : RequestMessage
    {
        Task Handle(TRequestMessage requestMessage);
    }

    public interface IRequestMessageWithStateHandler<in TRequestMessage, TResponseMessage>
        where TResponseMessage : ResponseMessage
        where TRequestMessage : RequestMessageWithState<TResponseMessage>
    {
        Task<TResponseMessage?> Handle(TRequestMessage request);
    }
}
