using CQRSlite.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BoundedContext.Commands
{
    public class RebuildSearchDbCommand : ICommand
    {
        public int ExpectedVersion { get; set; }
    }
}
