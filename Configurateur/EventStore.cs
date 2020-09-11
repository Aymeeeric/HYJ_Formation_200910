using System.Collections.Generic;

namespace Configurateur
{
    public class EventStore
    {
        public List<IEventWrapper> Events { get; set; } = new List<IEventWrapper>();

        public void Save(IEventWrapper wrapper)
        {
            Events.Add(wrapper);
        }
    }
}