using NEventStore.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Events.Polling
{
    public class CommitObserverStarter
    {
        private readonly IEnumerable<IObserveCommits> commitObservers;

        public CommitObserverStarter(IEnumerable<IObserveCommits> commitObservers)
        {
            this.commitObservers = commitObservers;
        }

        public void Start()
        {
            foreach (var observer in commitObservers)
            {
                observer.Start();
            }
        }
    }
}
