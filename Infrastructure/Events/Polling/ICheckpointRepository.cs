using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Events.Polling
{
    public interface ICheckpointRepository
    {
        string LoadCheckpoint();
        void SaveCheckpoint(string checkpointToken);
    }
}
