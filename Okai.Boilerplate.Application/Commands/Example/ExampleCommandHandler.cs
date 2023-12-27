using Okai.Boilerplate.Domain.Contracts;
using Okai.Boilerplate.Domain.Contracts.Data.RelationalDatabase;
using Okai.Boilerplate.Domain.Exceptions;
using Okai.Boilerplate.Domain.Mediator;

namespace Okai.Boilerplate.Application.Commands.Example
{
    public class ExampleCommandHandler :
        IRequestMessageWithStateHandler<ExampleCommand, ExampleState>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMediator _mediator;

        public ExampleCommandHandler(IRepositoryManager repositoryManager,
            IMediator mediator)
        {
            _repositoryManager = repositoryManager;
            _mediator = mediator;
        }

        public async Task<ExampleState?> Handle(ExampleCommand command)
        {
            var validation = command.Validate();

            if (!validation.IsValid())
                throw new CustomCommandException("Invalid Request!", validation.ValidationFailures);

            return new ExampleState("Command Example");
        }
    }
}
