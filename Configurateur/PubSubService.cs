using System.Collections.Generic;
using Configurateur;

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

        public void Handle(ConfigurationEventWrapper wrapper)
        {
            _eventStore.Save(wrapper);
            foreach (var projection in _projections)
            {
                projection.Apply(wrapper);
            }
        }
    }
}