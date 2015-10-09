using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Events.Versioning
{
    [AttributeUsage(AttributeTargets.Class)]
    public class VersionedEventAttribute : Attribute
    {
        public int Version { get; set; }
        public string Identifier { get; set; }

        public VersionedEventAttribute(string identifier, int version = 0)
        {
            this.Version = version;
            this.Identifier = identifier;
        }
    }
}
