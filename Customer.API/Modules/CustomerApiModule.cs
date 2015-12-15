using Autofac;
using CQRSlite.Bus;
using CQRSlite.Cache;
using CQRSlite.Commands;
using CQRSlite.Domain;
using CQRSlite.Events;
using Customer.BoundedContext.Commands;
using Customer.BoundedContext.Handlers;
using Customer.BoundedContext.ReadModel;
using Customer.BoundedContext.ReadModel.Handlers;
using FluentValidation;
using Infrastructure.Command;
using Infrastructure.EventStore;
using Infrastructure.Exceptions;
using Infrastructure.Repository;
using Infrastructure.Validation;
using Newtonsoft.Json;
using Raven.Client;
using Raven.Client.Embedded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Customer.API.Modules
{
    public class CustomerApiModule : Module
    {
        private IEnumerable<System.Reflection.Assembly> GetAvailableAssemblies()
        {
            yield return typeof(ActivateCustomer).Assembly;
            yield return typeof(Infrastructure.Exceptions.ValidationException).Assembly;
        }

        protected override void Load(ContainerBuilder builder)
        {

            var assembliesToCheck = 
                GetAvailableAssemblies()
                .ToArray();

            builder.RegisterType<InProcessBus>()
                .AsSelf()
                .As<ICommandSender>()
                .As<IEventPublisher>()
                .As<IHandlerRegistrar>()
                .SingleInstance();

            //builder.RegisterType<InMemoryEventStore>()
            //    .As<IEventStore>()
            //    .SingleInstance();
            builder.RegisterType<RavenDbEventStore>()
                .As<IEventStore>()
                .InstancePerRequest();

            builder.RegisterType<Session>()
                .As<ISession>()
                .InstancePerRequest();

            builder.Register((ctx) =>
            {
                var eventStore = ctx.Resolve<IEventStore>();
                var publisher = ctx.Resolve<IEventPublisher>();

                //return new CacheRepository(new Repository(eventStore, publisher), eventStore);
                return new Repository(eventStore, publisher);
            })
            .As<IRepository>()
            .InstancePerRequest();

            builder.RegisterAssemblyTypes(assembliesToCheck)
               .AsClosedTypesOf(typeof(IValidator<>))
               .InstancePerRequest();

            builder.RegisterType<ValidationFactory>()
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterType<CommandDispatcher>()
                .AsSelf()
                .InstancePerRequest();

            builder.RegisterType<CustomerCommandHandlers>()
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerRequest();

            builder.RegisterType<CustomerListEventHandlers>()
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerRequest();

            //builder.RegisterGeneric(typeof(MongoReadRepository<>))
            //    .As(typeof(IReadRepository<>));
            builder.RegisterGeneric(typeof(RavenDbReadRepository<>))
                .As(typeof(IReadRepository<>))
                .InstancePerRequest();

            builder.RegisterType<CustomerReadModelFacade>()
                .AsImplementedInterfaces()
                .InstancePerRequest();

            base.Load(builder);
        }
    }
}
