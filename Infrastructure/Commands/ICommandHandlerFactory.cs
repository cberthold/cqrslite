﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<T>[] GetHandlersForCommand<T>(T command);
    }
}
