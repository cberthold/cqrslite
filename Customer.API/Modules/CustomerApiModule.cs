using Autofac;
using CQRSlite.Bus;
using CQRSlite.Cache;
using CQRSlite.Commands;
using CQRSlite.Domain;
using CQRSlite.Events;
using Customer.BoundedContext.Handlers;
using Customer.BoundedContext.ReadModel;
using Customer.BoundedContext.ReadModel.Handlers;
using Infrastructure.Command;
using Infrastructure.EventStore;
using Infrastructure.Repository;
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
        protected override void Load(ContainerBuilder builder)
        {

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
                .SingleInstance();

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

            builder.RegisterType<CommandDispatcher>()
                .AsSelf();

            builder.RegisterType<CustomerCommandHandlers>()
                .AsImplementedInterfaces()
                .AsSelf();

            builder.RegisterType<CustomerListEventHandlers>()
                .AsImplementedInterfaces()
                .AsSelf();

            //builder.RegisterGeneric(typeof(MongoReadRepository<>))
            //    .As(typeof(IReadRepository<>));
            builder.RegisterGeneric(typeof(RavenDbReadRepository<>))
                .As(typeof(IReadRepository<>));

            builder.RegisterType<CustomerReadModelFacade>()
                .AsImplementedInterfaces();

            base.Load(builder);
        }
    }
}
