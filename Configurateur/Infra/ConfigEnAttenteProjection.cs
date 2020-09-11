using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configurateur.Infra
{
    public class ConfigEnAttenteProjection
    {
        public IList<Config> Configs { get; set; } = new List<Config>();

        public void Set(PublishedEvent publishedEvent)
        {
            if (publishedEvent.Event is ModeleSelectionne modeleSelectionne)
            {
                var newConfig = new Config()
                {
                    ConfigId = new ConfigId(publishedEvent.PublishedEventId.Id),
                    ModeleSelectionneeId = modeleSelectionne.ModeleId
                };
                Configs.Add(newConfig);
            }
        }
    }

    public struct Config
    {
        public ConfigId ConfigId { get; set; }
        public ModeleId ModeleSelectionneeId { get; set; }
    }

    public struct ConfigId
    {
        public Guid Id { get; set; }

        public ConfigId(Guid id)
        {
            Id = id;
        }
    }
}
