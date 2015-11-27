using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Events
{
    public abstract class EventBase<TEvent, TAggregate> : IEvent<TAggregate>, IEquatable<TEvent>
        where TAggregate : IAggregate<TAggregate>
        where TEvent : EventBase<TEvent, TAggregate>
    {
        public Guid Id { get; protected set;  }

        public EventBase(Guid id)
        {
            this.Id = id;
        }

        public override bool Equals(object obj)
        {

            if (obj == null)
                return false;

            TEvent other = obj as TEvent;

            if (other == null)
                return false;

            return Equals(other);
        }


        public override int GetHashCode()
        {
            IEnumerable<FieldInfo> fields = GetFields();

            int startValue = 17;
            int multiplier = 59;
            int hashCode = startValue;
            
            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(this);

                if (value != null)
                    hashCode = hashCode * multiplier + value.GetHashCode();
            }

            return hashCode;
        }


        private IEnumerable<FieldInfo> GetFields()
        {
            Type t = GetType();
            
            List<FieldInfo> fields = new List<FieldInfo>();

            while (t != typeof(object))
            {
                fields.AddRange(t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));
                t = t.BaseType;
            }
            
            return fields;
        }

        public virtual bool Equals(TEvent other)
        {
            if (other == null)
                return false;
            
            Type t = GetType();
            Type otherType = other.GetType();

            if (t != otherType)
                return false;

            FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (FieldInfo field in fields)
            {
                object value1 = field.GetValue(other);
                object value2 = field.GetValue(this);
                
                if (value1 == null)
                {
                    if (value2 != null)
                        return false;
                }
                else if (!value1.Equals(value2))
                    return false;
            }
            
            return true;
        }

        public static bool operator ==(EventBase<TEvent, TAggregate> x, EventBase<TEvent, TAggregate> y)
        {
            if (!(x is EventBase<TEvent, TAggregate>) && (!(y is EventBase<TEvent, TAggregate>))) return true;

            return x.Equals(y);
        }
        
        public static bool operator !=(EventBase<TEvent, TAggregate> x, EventBase<TEvent, TAggregate> y)
        {
            return !(x == y);
        }

    }
    

}
