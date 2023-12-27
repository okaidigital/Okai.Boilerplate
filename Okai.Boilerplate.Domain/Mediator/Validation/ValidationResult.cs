using System.Text.Json;

namespace Okai.Boilerplate.Domain.Mediator.Validation
{
    public class ValidationResult
    {
        public IList<ValidationFailure> ValidationFailures { get; set; }

        public ValidationResult()
        {
            ValidationFailures = new List<ValidationFailure>();
        }

        public void AddError(IList<string> errors, string propertyName, string valueAttempted)
        {
            ValidationFailure failure = new(errors, propertyName, valueAttempted);

            ValidationFailures.Add(failure);
        }
        public string ToJson() => JsonSerializer.Serialize(ValidationFailures);
        public bool IsValid() => !ValidationFailures.Any();
    }
}
