using System.Collections.Generic;

namespace Configurateur
{
    public interface IEventStore

    {
        List<IEventWrapper> Events { get; set; }
        void SaveEvents(List<IEventWrapper> wrappers);
        List<IEvent> GetAllEventsForId(string id);

    }
}