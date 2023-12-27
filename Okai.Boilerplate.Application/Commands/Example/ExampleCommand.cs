using Okai.Boilerplate.Domain.Mediator.Abstract;
using Okai.Boilerplate.Domain.Mediator.Validation;

namespace Okai.Boilerplate.Application.Commands.Example
{
    public class ExampleCommand : MessageWithStateCommand<ExampleState>
    {
        public ExampleCommand() { }

        public override ValidationResult Validate()
        {
            return new ValidationResult();
        }
    }
}
