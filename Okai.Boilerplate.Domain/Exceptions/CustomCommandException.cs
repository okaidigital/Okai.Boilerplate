using Okai.Boilerplate.Domain.Mediator.Validation;

namespace Okai.Boilerplate.Domain.Exceptions
{
    public class CustomCommandException : Exception
    {
        public IList<ValidationFailure> ValidationFailures { get; set; }

        public CustomCommandException(string message, IList<ValidationFailure> validationFailures) : base(message)
        {
            ValidationFailures = validationFailures;
        }
    }
}
