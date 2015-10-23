using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
    public interface IHandle<in TCommand>
        where TCommand : ICommand
    {
        IAggregate Handle(TCommand command);
    }
}
