using Okai.Boilerplate.Domain.Mediator.Abstract;

namespace Okai.Boilerplate.Application.Commands.Example
{
    public class ExampleState : State
    {
        public string Message { get; set; } = string.Empty;

        public ExampleState() { }

        public ExampleState(string message)
        {
            Message = message;
        }
    }
}
