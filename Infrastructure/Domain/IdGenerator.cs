using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public class IdGenerator
    {
        private static Func<Guid> _generator;

        public static Func<Guid> GenerateGuid
        {
            get
            {
                _generator = _generator ?? Guid.NewGuid;
                return _generator;
            }
            set { _generator = value; }
        }
    }
}
