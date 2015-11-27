using Infrastructure.Domain;
using Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
    public class CommandDispatcher
    {
        private Dictionary<Type, Action<ICommand>> _routes;

        public CommandDispatcher()
        {
           _routes = new Dictionary<Type, Action<ICommand>>();
        }

        public void RegisterHandler<TCommand>(IHandle<TCommand> handler) where TCommand : class, ICommand<TCommand>
        {
            var handler1 = handler;
            Action<ICommand> action = (command) => handler1.Handle(command as TCommand);
            _routes.Add(typeof(TCommand), action);
        }

        public void ExecuteCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandType = command.GetType();

            if (!_routes.ContainsKey(commandType))
            {
                throw new ApplicationException("Missing handler for " + commandType.Name);
            }
            _routes[commandType](command);
        }

        

    }
}
