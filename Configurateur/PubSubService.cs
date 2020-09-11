using Configurateur;

namespace Configurateur
{
    public class PubSubService
    {
        private readonly EventStore _eventStore;

        public PubSubService(EventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public void Handle(ConfigurationEventWrapper wrapper)
        {
            _eventStore.Save(wrapper);
        }
    }
}