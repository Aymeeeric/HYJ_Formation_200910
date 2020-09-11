using System.Collections.Generic;
using System.Linq;

namespace Configurateur
{
    public class InMemoryEventStore : IEventStore
    {
        public List<IEventWrapper> Events { get; set; }=new List<IEventWrapper>();

        public List<IEvent> GetAllEventsForId(string id)
        {
            return Events.
                Where(wrp => wrp.GetId().Equals(id))
                .Select(wrp => wrp.Event)
                .ToList();
        }

        public void SaveEvents(List<IEventWrapper> wrappers)
        {
            Events.AddRange(wrappers);
        }
    }
}