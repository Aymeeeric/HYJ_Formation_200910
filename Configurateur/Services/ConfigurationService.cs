using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configurateur.Services
{
    public class ConfigurationService
    {
        private readonly EventStore _eventStore;
        private readonly PubSubService _pubSubService;

        public ConfigurationService(EventStore eventStore, PubSubService pubSubService)
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
