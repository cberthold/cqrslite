using Infrastructure.Commands;
using Infrastructure.Domain;
using Infrastructure.Tests.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tests
{
    public class TestDomainEntry
    {
        private readonly CommandDispatcher _commandDispatcher;

        public TestDomainEntry(IDomainRepository domainRepository)
        {
            _commandDispatcher = CreateCommandDispatcher(domainRepository);
        }

        public void ExecuteCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            _commandDispatcher.ExecuteCommand(command);
        }

        private CommandDispatcher CreateCommandDispatcher(IDomainRepository domainRepository)
        {
            var commandDispatcher = new CommandDispatcher();

            var domainObjectHandler = new TestDomainObjectCommandHandler(domainRepository);
            commandDispatcher.RegisterHandler<CreateDomainObject>(domainObjectHandler);
            commandDispatcher.RegisterHandler<RenameDomainObject>(domainObjectHandler);
            
            return commandDispatcher;
        }
        
    }
}
