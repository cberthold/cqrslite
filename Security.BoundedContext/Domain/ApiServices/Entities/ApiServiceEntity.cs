using Infrastructure.Domain;
using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain
{
    /// <summary>
    /// Entity represents the 
    /// </summary>
    public class ApiServiceEntity : IEntity
    {
        public static readonly Guid SECURITY_API = new Guid(@"4D04D504-71A5-4D2A-8A4E-16327F0D6BD7");
        public static readonly Guid CUSTOMER_API = new Guid(@"09B1B02B-9369-4AAB-A91A-0DFACC51F86F");
        public static readonly Guid SIGNALR_API = new Guid(@"138CC799-0FF5-42C1-A7FA-099ECA359F9E");

        public const string SECURITY_API_NAME = "Security API";
        public const string CUSTOMER_API_NAME = "Customer API";
        public const string SIGNALR_API_NAME = "SignalR API";

        public Guid Id { get; protected set; }
        public string Name { get; protected set; }

        IDictionary<Guid, ResourceActionEntity> _resourceActions;

        public ApiServiceEntity(Guid entityId, string name)
        {
            _resourceActions = new Dictionary<Guid, ResourceActionEntity>();

            this.Id = entityId;
            this.Name = name;
        }

        public ResourceActionEntity FindResourceAction(string resourceName, string actionName)
        {
            var resourceAction = _resourceActions.Values.FirstOrDefault(a => a.ResourceName == resourceName && a.ActionName == actionName);

            return resourceAction;
        }

        public ResourceActionEntity AddResourceAction(string resourceName, string actionName)
        {
            var newId = Guid.NewGuid();

            var resourceAction = ResourceActionEntity.Create(newId, Id, resourceName, actionName);

            _resourceActions[newId] = resourceAction;

            return resourceAction;
        }

        public ResourceActionEntity FindResourceAction(Guid entityId)
        {
            if (_resourceActions.ContainsKey(entityId))
                return _resourceActions[entityId];

            return null;
        }

        public void RenameResourceActionResourceName(Guid entityId, string resourceName)
        {

        }

        public bool IsDuplicateResourceAction(string resourceName, string actionName, Guid? entityIdToExclude = null)
        {
            if(entityIdToExclude == null)
            {
                return _resourceActions.Any(
                        a=>a.Value.ResourceName == resourceName
                        && a.Value.ActionName == actionName);
            }
            else
            {
                return _resourceActions.Any(a => 
                        a.Key !=  entityIdToExclude.Value
                        && a.Value.ResourceName == resourceName
                        && a.Value.ActionName == actionName);
            }
            
        }

    }
}
