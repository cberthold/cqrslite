﻿using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.RootPolicy.Services
{
    public interface IPolicyService : IDomainService
    {
        TRoot LoadRootPolicy<TRoot>(Guid id)
            where TRoot : RootPolicyAggregate<TRoot>;
    }
}
