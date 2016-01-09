using CQRSlite.Domain;
using CQRSlite.Domain.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public class HookableSession : ISession
    {
        private readonly IRepository _repository;
        private readonly Dictionary<Guid, AggregateDescriptor> _trackedAggregates;
        private readonly IEnumerable<ISessionHook> _sessionHooks;

        public HookableSession(IRepository repository, IEnumerable<ISessionHook> sessionHooks)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            _repository = repository;
            _trackedAggregates = new Dictionary<Guid, AggregateDescriptor>();

            _sessionHooks = (sessionHooks ?? new ISessionHook[0])
                .Where(hook => hook != null)
                .ToArray();
        }

        public void Add<T>(T aggregate) where T : AggregateRoot
        {
            if (!IsTracked(aggregate.Id))
                _trackedAggregates.Add(aggregate.Id,
                    new AggregateDescriptor { Aggregate = aggregate, Version = aggregate.Version });
            else if (_trackedAggregates[aggregate.Id].Aggregate != aggregate)
                throw new ConcurrencyException(aggregate.Id);

            foreach(var hook in _sessionHooks)
            {
                hook.AddAggregate(aggregate);
            }
        }

        public T Get<T>(Guid id, int? expectedVersion = null) where T : AggregateRoot
        {
            if (IsTracked(id))
            {
                var trackedAggregate = (T)_trackedAggregates[id].Aggregate;
                if (expectedVersion != null && trackedAggregate.Version != expectedVersion)
                    throw new ConcurrencyException(trackedAggregate.Id);
                return trackedAggregate;
            }

            var aggregate = _repository.Get<T>(id);
            if (expectedVersion != null && aggregate.Version != expectedVersion)
                throw new ConcurrencyException(id);
            Add(aggregate);

            return aggregate;
        }

        private bool IsTracked(Guid id)
        {
            return _trackedAggregates.ContainsKey(id);
        }

        public void Commit()
        {
            // execute pre-commit hooks on tracked aggregates
            foreach(var hook in _sessionHooks)
            {
                hook.PreCommit(_trackedAggregates.Values);
            }

            foreach (var descriptor in _trackedAggregates.Values)
            {
                _repository.Save(descriptor.Aggregate, descriptor.Version);
            }
            _trackedAggregates.Clear();

            // execute post-commit hooks
            foreach (var hook in _sessionHooks)
            {
                hook.PostCommit();
            }
        }

        public class AggregateDescriptor
        {
            public AggregateRoot Aggregate { get; set; }
            public int Version { get; set; }
        }
    }
}
