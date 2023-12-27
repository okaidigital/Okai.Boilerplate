using FluentValidation;

namespace Okai.Boilerplate.Domain.Mediator.Abstract
{
    public abstract class Validator<TClass> : AbstractValidator<TClass> where TClass : class
    {
        public bool IsValid(TClass entity)
        {
            return Validate(entity).IsValid;
        }
    }
}
