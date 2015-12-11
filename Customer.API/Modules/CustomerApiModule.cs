using Autofac;
using CQRSlite.Bus;
using CQRSlite.Cache;
using CQRSlite.Commands;
using CQRSlite.Domain;
using CQRSlite.Events;
using Customer.BoundedContext.Handlers;
using Customer.BoundedContext.ReadModel;
using Customer.BoundedContext.ReadModel.Handlers;
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


            //x.For<IRepository>().HybridHttpOrThreadLocalScoped().Use(y =>
            //    new CacheRepository(new Repository(y.GetInstance<IEventStore>(), y.GetInstance<IEventPublisher>()),
            //        y.GetInstance<IEventStore>()));

            //x.Scan(s =>
            //{
            //    s.AssemblyContainingType<SmDependencyResolver>();
            //    s.AssemblyContainingType<ReadModelFacade>();
            //    s.Convention<FirstInterfaceConvention>();
            //});
            base.Load(builder);
        }
    }

    public class RavenDbEventStore : IEventStore
    {
        private static IDocumentStore store;
        private static JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Objects
        };

        static RavenDbEventStore()
        {
            store = new EmbeddableDocumentStore { ConnectionStringName = "EventStoreDb" };
            store.Conventions.AllowQueriesOnId = true;
            store.Initialize();
        }

        public IEnumerable<IEvent> Get(Guid aggregateId, int fromVersion)
        {
            using (var session = store.OpenSession())
            {
                var documents = from d in session.Query<DocumentData>()
                             where d.AggregateId == aggregateId
                             && d.Version > fromVersion
                             orderby d.Version
                             select d;

                Func<string, IEvent> func = (data) =>
                {
                    var output = JsonConvert.DeserializeObject(data, settings);

                    return (IEvent)output;
                };


                var events = from e in documents.ToList()
                             select func(e.EventData);
                


                return events.Cast<IEvent>();
            }
        }

        public void Save(IEvent @event)
        {
            var document = new DocumentData()
            {
                CommitId = Guid.NewGuid(),
                AggregateId = @event.Id,
                Version = @event.Version,
                EventData = JsonConvert.SerializeObject(@event, settings)
            };

            using (var session = store.OpenSession())
            {
                session.Store(document);
                session.SaveChanges();
            }
        }

        public class DocumentData
        {
            public Guid CommitId { get; set; }
            public Guid AggregateId { get; set; }
            public int Version { get; set; }
            public string EventData { get; set; }
        }
        
    }

    public class InMemoryEventStore : IEventStore
    {
        private readonly Dictionary<Guid, List<IEvent>> _inMemoryDB = new Dictionary<Guid, List<IEvent>>();

        public IEnumerable<IEvent> Get(Guid aggregateId, int fromVersion)
        {
            List<IEvent> events;
            _inMemoryDB.TryGetValue(aggregateId, out events);
            return events?.Where(x => x.Version > fromVersion) ?? new List<IEvent>();
        }

        public void Save(IEvent @event)
        {
            List<IEvent> list;
            _inMemoryDB.TryGetValue(@event.Id, out list);
            if (list == null)
            {
                list = new List<IEvent>();
                _inMemoryDB.Add(@event.Id, list);
            }
            list.Add(@event);
        }
    }
}
