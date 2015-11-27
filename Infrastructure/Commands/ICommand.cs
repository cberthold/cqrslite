using Infrastructure.Domain;
using Infrastructure.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
    public interface ICommand<TCommand> : ICommand
        where TCommand : ICommand<TCommand>
    {
    }

    public interface ICommand : IMessage { } 
}
