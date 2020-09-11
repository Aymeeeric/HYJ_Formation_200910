using System.Linq;

namespace Configurateur.Services
{
    public class ConfigurationService
    {
        private readonly IEventStore _eventStore;
        private readonly PubSubService _pubSubService;

        public ConfigurationService(IEventStore eventStore, PubSubService pubSubService)
        {
            _eventStore = eventStore;
            _pubSubService = pubSubService;
        }

        public void SelectionneModele(ConfigurationId configId)
        {
            var history = _eventStore.GetAllEventsForId(configId.Id);
            var configAggregate = new Configuration(history);

            var events = configAggregate.SelectionneModele();

            if (events.Any())
            {
                _pubSubService.Handle(events.Select(
                        evt => (IEventWrapper)new ConfigurationEventWrapper()
                        {
                            ConfigurationId = configId,
                            Event = evt
                        }
                    )
                    .ToList()
                );
            }
        }
    }
}