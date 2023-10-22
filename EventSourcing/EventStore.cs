using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    public class EventStore
    {
        private readonly Dictionary<Guid, List<object>> _eventStore = new Dictionary<Guid, List<object>>();

        public void SaveEvents(Guid aggregateId, List<object> events)
        {
            if (!_eventStore.ContainsKey(aggregateId))
            {
                _eventStore[aggregateId] = new List<object>();
            }
            _eventStore[aggregateId].AddRange(events);
        }

        public List<object> GetEvents(Guid aggregateId)
        {
            if (_eventStore.ContainsKey(aggregateId))
            {
                return _eventStore[aggregateId];
            }
            return new List<object>();
        }
    }
}
