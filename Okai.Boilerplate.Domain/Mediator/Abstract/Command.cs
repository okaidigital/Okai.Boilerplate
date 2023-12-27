using Okai.Boilerplate.Domain.Mediator.Validation;
using System.Text.Json.Serialization;

namespace Okai.Boilerplate.Domain.Mediator.Abstract
{
    public abstract class Command : RequestMessage
    {
        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            ValidationResult = new ValidationResult();
        }

        public abstract bool IsValid();
    }
}
