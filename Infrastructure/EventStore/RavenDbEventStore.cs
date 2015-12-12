using CQRSlite.Events;
using Newtonsoft.Json;
using Raven.Client;
using Raven.Client.Embedded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EventStore
{
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
}
