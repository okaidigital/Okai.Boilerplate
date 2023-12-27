using Okai.Boilerplate.Domain.Mediator.Validation;

namespace Okai.Boilerplate.Domain.Mediator.Abstract
{
    public abstract class MessageWithStateCommand<TResponseMessage> : RequestMessageWithState<TResponseMessage> where TResponseMessage : State
    {
        public bool IsValid<TValidator, TEntity>(TValidator validator) where TValidator : Validator<TEntity>, new()
            where TEntity : MessageWithStateCommand<TResponseMessage>
        {
            return validator.IsValid((TEntity)this);
        }

        public abstract ValidationResult Validate();
    }
}
