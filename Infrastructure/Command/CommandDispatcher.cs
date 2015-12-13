using CQRSlite.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Command
{
    public class CommandDispatcher
    {
        ICommandSender bus;

        public CommandDispatcher(ICommandSender bus)
        {
            this.bus = bus;
        }

        public void Send<T>(T command) 
            where T : ICommand
        {
            bus.Send(command);
        }
    }
}
