using Infrastructure.Domain;
using Infrastructure.Exceptions;
using Security.BoundedContext.Domain.RootPolicy.Aggregate;
using Security.BoundedContext.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.RootPolicy.Entities
{
    //public class FeatureToAddToPolicy : IEntity
    //{
    //    public Guid Id { get; private set; }
    //    public IPolicy Policy { get; private set; }
    //    public FeatureAggregate Feature { get; private set; }
    //    public FeatureState State { get; private set; }
    //    public bool IsParentEnabled { get; private set; }
    //    public bool IsEnabled => CalculateIsEnabled();

    //    public FeatureToAddToPolicy(IPolicy policy, FeatureAggregate feature) {
    //        Id = feature.Id;
    //        Policy = policy;
    //        Feature = feature;
    //        State = FeatureState.Inherited;
    //        IsParentEnabled = false;
    //    }

    //    public void SetIsParentEnabledFromParentPolicy(IPolicy parentPolicy)
    //    {
    //        if (!parentPolicy.ChildPolicies.Contains(Policy))
    //            throw new DomainException($"parentPolicy is not a parent of the policy associated with Policy {Policy.Id}");
    //        IsParentEnabled = parentPolicy.AddedFeatures[Id].IsEnabled;
    //    }

    //    public bool CalculateIsEnabled()
    //    {
    //        // force disabled
    //        if (State == FeatureState.ForcedDisabled) return false;
    //        // force enabled
    //        else if (State == FeatureState.ForcedEnabled) return true;
    //        // inherited from parent
    //        else return IsParentEnabled;
    //    }
    //}
}
