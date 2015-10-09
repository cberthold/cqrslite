using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
    [Serializable]
    public abstract class Command : IMessage
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
    }
}
