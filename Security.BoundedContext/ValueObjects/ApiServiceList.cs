using CQRSlite.Domain;
using Infrastructure.Exceptions;
using Security.BoundedContext.Events;
using Security.BoundedContext.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Security.BoundedContext.Domain.Api.Aggregate;

namespace Security.BoundedContext.Domain
{
    public class ApiServiceList : IList<ApiServiceListValueObject>
    {
        IList<ApiServiceListValueObject> internalList;

        public ApiServiceList()
        {
            var list = new List<ApiServiceListValueObject>()
            {
                new ApiServiceListValueObject()
                {
                    ServiceId = Constants.CUSTOMER_API,
                    ServiceName = Constants.CUSTOMER_API_NAME,
                },
                new ApiServiceListValueObject()
                {
                    ServiceId = Constants.SECURITY_API,
                    ServiceName = Constants.SECURITY_API_NAME,
                },
                new ApiServiceListValueObject()
                {
                    ServiceId = Constants.SIGNALR_API,
                    ServiceName = Constants.SIGNALR_API_NAME,
                },
            };

            internalList = list.AsReadOnly();
        }

        public ApiServiceListValueObject this[int index]
        {
            get { return internalList[index]; }
            set { internalList[index] = value; }
        }

        public int Count
        {
            get { return internalList.Count; }
        }

        public bool IsReadOnly
        {
            get { return internalList.IsReadOnly; }
        }

        public void Add(ApiServiceListValueObject item)
        {
            internalList.Add(item);
        }

        public void Clear()
        {
            internalList.Clear();
        }

        public bool Contains(ApiServiceListValueObject item)
        {
            return internalList.Contains(item);
        }

        public void CopyTo(ApiServiceListValueObject[] array, int arrayIndex)
        {
            internalList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ApiServiceListValueObject> GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        public int IndexOf(ApiServiceListValueObject item)
        {
            return internalList.IndexOf(item);
        }

        public void Insert(int index, ApiServiceListValueObject item)
        {
            internalList.Insert(index, item);
        }

        public bool Remove(ApiServiceListValueObject item)
        {
            return internalList.Remove(item);
        }

        public void RemoveAt(int index)
        {
            internalList.RemoveAt(index);    
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return internalList.GetEnumerator();
        }
    }
}
