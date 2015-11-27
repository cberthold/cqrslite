﻿using Infrastructure.Domain;
using Infrastructure.Events;
using Infrastructure.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public class InMemoryDomainRespository : DomainRepositoryBase
    {
        public Dictionary<Guid, List<string>> _eventStore = new Dictionary<Guid, List<string>>();
        private List<IEvent> _latestEvents = new List<IEvent>();
        private JsonSerializerSettings _serializationSettings;

        public InMemoryDomainRespository()
        {
            _serializationSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
        }
        

        private string Serialize(IEvent arg)
        {
            var outputString = JsonConvert.SerializeObject(arg, _serializationSettings);
            return outputString;
        }

        public IEnumerable<IEvent> GetLatestEvents()
        {
            return _latestEvents;
        }

        public override TResult GetById<TResult>(Guid id)
        {
            if (_eventStore.ContainsKey(id))
            {
                var events = _eventStore[id];
                var deserializedEvents = events.Select(e => JsonConvert.DeserializeObject(e, _serializationSettings) as IEvent<TResult>);
                return BuildAggregate<TResult>(deserializedEvents);
            }
            throw new AggregateNotFoundException("Could not found aggregate of type " + typeof(TResult) + " and id " + id);
        }

        public void AddEvents(Dictionary<Guid, IEnumerable<IEvent>> eventsForAggregates)
        {
            foreach (var eventsForAggregate in eventsForAggregates)
            {
                _eventStore.Add(eventsForAggregate.Key, eventsForAggregate.Value.Select(Serialize).ToList());
            }
        }

        public override IEnumerable<IEvent<TAggregate>> Save<TAggregate>(TAggregate aggregate)
        {
            var eventsToSave = aggregate.GetUncommittedEvents().ToList();
            var serializedEvents = eventsToSave.Select(Serialize).ToList();
            var expectedVersion = CalculateExpectedVersion(aggregate, eventsToSave);
            if (expectedVersion == 0)
            {
                _eventStore.Add(aggregate.Id, serializedEvents);
            }
            else
            {
                var existingEvents = _eventStore[aggregate.Id];
                var currentversion = existingEvents.Count;
                if (currentversion != expectedVersion)
                {
                    throw new WrongExpectedVersionException("Expected version " + expectedVersion +
                                                            " but the version is " + currentversion);
                }
                existingEvents.AddRange(serializedEvents);
            }
            _latestEvents.AddRange(eventsToSave);
            aggregate.ClearUncommittedEvents();
            return eventsToSave;
        }
        
    }
}
