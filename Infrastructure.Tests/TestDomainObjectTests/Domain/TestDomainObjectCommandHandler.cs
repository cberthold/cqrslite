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
        private readonly IDomainRepository domainRepository;

        public TestDomainObjectCommandHandler(IDomainRepository domainRepository)
        {
            this.domainRepository = domainRepository;
        }

        public void Handle(CreateDomainObject command)
        {
            try
            {
                var customer = domainRepository.GetById<TestDomainObject>(command.Id);
                throw new AggregateAlreadyExistsException<TestDomainObject>(command.Id);
            }
            catch (AggregateNotFoundException)
            {
                // We expect not to find anything
            }
            var aggregate = TestDomainObject.Create(command.Id, command.Name);
            domainRepository.Save(aggregate);
        }

        public void Handle(RenameDomainObject command)
        {
            var domainObject = domainRepository.GetById<TestDomainObject>(command.Id);
            domainObject.Rename(command.Name);
            domainRepository.Save(domainObject);
        }
    }
}
