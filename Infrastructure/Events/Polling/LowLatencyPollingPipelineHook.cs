using NEventStore;
using NEventStore.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Events.Polling
{
    public class LowLatencyPollingPipelineHook : PipelineHookBase
    {
        private readonly Lazy<IObserveCommits> commitsObserver;

        public LowLatencyPollingPipelineHook(Lazy<IObserveCommits> commitsObserver)
        {
            Contract.Requires<ArgumentNullException>(commitsObserver != null, "commitsObserver");
            this.commitsObserver = commitsObserver;
        }

        public override void PostCommit(ICommit committed)
        {
            base.PostCommit(committed);
            commitsObserver.Value.PollNow();
        }
    }
}
