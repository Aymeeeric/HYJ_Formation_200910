using System.Collections.Generic;

namespace Configurateur
{
    public class PubSubService
    {
        private readonly EventStore _eventStore;
        private readonly List<IProjection> _projections;

        public PubSubService(EventStore eventStore, List<IProjection> projections)
        {
            _eventStore = eventStore;
            _projections = projections;
        }

        public void Handle(List<IEventWrapper> wrappers)
        {
            _eventStore.Save(wrappers);

            foreach (var eventWrapper in wrappers)
            {
                foreach (var projection in _projections)
                {
                    projection.Apply(eventWrapper);
                }
            }
        }
    }
}