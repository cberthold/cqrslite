using Autofac;
using CQRSlite.Bus;
using CQRSlite.Commands;
using CQRSlite.Domain;
using CQRSlite.Events;
using Infrastructure.Command;
using Infrastructure.Domain;
using Infrastructure.EventStore;
using Infrastructure.Validation;
using Security.BoundedContext.Domain;
using Security.BoundedContext.Domain.Api.Services;
using Security.BoundedContext.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Tests
{
    public class TestModule : Module
    {
        private IEnumerable<System.Reflection.Assembly> GetAvailableAssemblies()
        {
            yield return typeof(ApiServiceList).Assembly;
            yield return typeof(ApiServiceCreated).Assembly;
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

            builder.RegisterType<InMemoryEventStore>()
                .As<IEventStore>()
                .InstancePerRequest();
            //builder.RegisterType<RavenDbEventStore>()
            //    .As<IEventStore>()
            //    .InstancePerRequest();

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

            //builder.RegisterAssemblyTypes(assembliesToCheck)
            //   .AsClosedTypesOf(typeof(IValidator<>))
            //   .InstancePerRequest();

            builder.RegisterType<ValidationFactory>()
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterType<CommandDispatcher>()
                .AsSelf()
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(IApiService).Assembly)
                .Where(t => {
                    return typeof(IDomainService).IsAssignableFrom(t);
                    })
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerRequest();

            //builder.RegisterType<CustomerCommandHandlers>()
            //    .AsImplementedInterfaces()
            //    .AsSelf()
            //    .InstancePerRequest();

            //builder.RegisterType<CustomerListEventHandlers>()
            //    .AsImplementedInterfaces()
            //    .AsSelf()
            //    .InstancePerRequest();

            //builder.RegisterGeneric(typeof(MongoReadRepository<>))
            //    .As(typeof(IReadRepository<>));
            //builder.RegisterGeneric(typeof(RavenDbReadRepository<>))
            //    .As(typeof(IReadRepository<>))
            //    .InstancePerRequest();

            //builder.RegisterType<CustomerReadModelFacade>()
            //    .AsImplementedInterfaces()
            //    .InstancePerRequest();

            base.Load(builder);
        }
    }
}
