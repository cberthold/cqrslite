using CommonDomain;
using Infrastructure.Commands;
using Infrastructure.Domain;
using Infrastructure.Exceptions;
using Infrastructure.Tests.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tests
{
    internal class TestDomainObjectCommandHandler :
    IHandle<CreateDomainObject>,
    IHandle<RenameDomainObject>
    {
        private readonly IDomainRepository _domainRepository;

        public TestDomainObjectCommandHandler(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public IAggregate Handle(CreateDomainObject command)
        {
            try
            {
                var customer = _domainRepository.GetById<TestDomainObject>(command.Id);
                throw new AggregateAlreadyExistsException<TestDomainObject>(command.Id);
            }
            catch (AggregateNotFoundException)
            {
                // We expect not to find anything
            }
            return TestDomainObject.Create(command.Id, command.Name);
        }

        public IAggregate Handle(RenameDomainObject command)
        {
            var domainObject = _domainRepository.GetById<TestDomainObject>(command.Id);
            domainObject.Rename(command.Name);
            return domainObject;
        }
    }
}
