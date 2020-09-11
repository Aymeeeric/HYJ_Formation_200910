using System.Collections.Generic;
using System.Text;

namespace Configurateur
{
    public struct ConfigurationEventWrapper : IEventWrapper
    {
        public IEvent Event { get; set; }
        public ConfigurationId ConfigurationId { get; set; }
    }
}
