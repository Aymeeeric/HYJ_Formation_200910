using System.Collections.Generic;
using System.Linq;

namespace Configurateur
{
    public class EventStore
    {
        public List<IEventWrapper> Events { get; set; } = new List<IEventWrapper>();

        public void Save(List<IEventWrapper> wrappers)
        {
            Events.AddRange(wrappers);
        }

        public List<IEvent> GetAllEventsForId(string id)
        {
            return Events.
                Where(wrp => wrp.GetId().Equals(id))
                .Select(wrp => wrp.Event)
                .ToList();
        }
    }
}