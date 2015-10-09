using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Events
{
    [Serializable]
    public abstract class Event : IMessage
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
    }
}
