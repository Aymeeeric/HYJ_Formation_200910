using System.Collections.Generic;

namespace Configurateur
{
    public class ConfigEnAttenteProjection:IProjection
    {
        public IList<Config> Configs { get; set; } = new List<Config>();

        public void Apply(IEventWrapper eventWrapper)
        {
            if (eventWrapper is ConfigurationEventWrapper configEnvent)
            {
                if (configEnvent.Event is ModeleSelectionne)
                    PlayModeleSelectionneeEvent(configEnvent);
            }
        }

        public void PlayModeleSelectionneeEvent(ConfigurationEventWrapper configEnvent)
        {
            var modeleSelectionne = (ModeleSelectionne)configEnvent.Event;

            var newConfig = new Config()
            {
                ConfigId = configEnvent.ConfigurationId,
                ModeleSelectionneeId = modeleSelectionne.ModeleId
            };

            Configs.Add(newConfig);
        }
    }

}