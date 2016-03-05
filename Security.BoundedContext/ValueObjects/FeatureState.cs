﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.ValueObjects
{
    public enum FeatureState
    {
        ForcedEnabled = 1,
        Inherited = 0,
        ForcedDisabled = -1
    }
}
