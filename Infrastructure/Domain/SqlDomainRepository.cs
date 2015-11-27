using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Events;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using Newtonsoft.Json;
using Infrastructure.Exceptions;

namespace Infrastructure.Domain
{
    public class SqlDomainRepository : DomainRepositoryBase
    {
        private string connectionStringName;

        public SqlDomainRepository(string connectionStringName)
        {
            this.connectionStringName = connectionStringName;

        }

        private string Serialize(IEvent arg)
        {
            var outputString = JsonConvert.SerializeObject(arg, SqlEventExtensions.SerializerSettings);
            return outputString;
        }

        public override TResult GetById<TResult>(Guid id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

            using (var conn = new SqlConnection(connectionString))
            {
                const string sql = "SELECT * FROM Events WHERE AggregateId=@id";
                var listOfEventData = conn.Query<SqlEventData>(sql, new { id });
                var events = listOfEventData.Select(x => x.DeserializeEvent<TResult>());

                if(events == null || events.Count() == 0)
                {
                    throw new AggregateNotFoundException("Could not found aggregate of type " + typeof(TResult) + " and id " + id);
                }

                var aggregate = BuildAggregate<TResult>(events);
                return aggregate;
            }
        }

        public override IEnumerable<IEvent<TAggregate>> Save<TAggregate>(TAggregate aggregate)
        {
            var events = aggregate.GetUncommittedEvents().ToList();

            if (events.Count == 0)
                return events;
            
            var expectedVersion = CalculateExpectedVersion(aggregate, events);

            var aggregateType = aggregate.GetType().Name;
            var originalVersion = aggregate.Version - events.Count() + 1;
            var eventsToSave = events
                .Select(e => e.ToEventData(aggregateType, aggregate.Id, originalVersion++))
                .ToArray();

            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    var foundVersion = (int?)conn.ExecuteScalar(
                        "SELECT MAX(Version) FROM Events WHERE AggregateId=@aggregateId",
                        new { aggregateId = aggregate.Id },
                        tx
                    );
                    if (foundVersion.HasValue && foundVersion >= originalVersion)
                        throw new Exception("Concurrency exception");

                    const string sql =
                        @"INSERT INTO Events(Id, Created, AggregateType, AggregateId, Version, Event, Metadata) 
                  VALUES(@Id, @Created, @AggregateType, @AggregateId, @Version, @Event, @Metadata)";
                    conn.Execute(sql, eventsToSave, tx);
                    tx.Commit();
                }
            }
            aggregate.ClearUncommittedEvents();
            return events;
            
        }
    }

    public class SqlEventData
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string AggregateType { get; set; }
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
        public string Event { get; set; }
        public string Metadata { get; set; }
    }

    public static class SqlEventExtensions
    {
        public static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj, SerializerSettings);
        }

        public static IEvent<TAggregate> DeserializeEvent<TAggregate>(this SqlEventData eventData)
            where TAggregate : IAggregate<TAggregate>
        {
            return JsonConvert.DeserializeObject(eventData.Event) as IEvent<TAggregate>;
        }

        public static SqlEventData ToEventData(this object @event, string aggregateType, Guid aggregateId, int version)
        {
            var data = SerializeObject(@event);
            var eventHeaders = new Dictionary<string, object>
            {
                {
                    "EventClrType", @event.GetType().AssemblyQualifiedName
                }
            };
            var metadata = SerializeObject(eventHeaders);
            var eventId = SequentialGuid.NewSequentialGuid();

            return new SqlEventData
            {
                Id = eventId,
                Created = DateTime.UtcNow,
                AggregateType = aggregateType,
                AggregateId = aggregateId,
                Version = version,
                Event = data,
                Metadata = metadata,
            };
        }
    }
}
