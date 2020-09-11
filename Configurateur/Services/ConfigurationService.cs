using System;
using System.Collections.Generic;
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

        public void SelectionneModele()
        {
            throw new NotImplementedException();
        }
    }
}
