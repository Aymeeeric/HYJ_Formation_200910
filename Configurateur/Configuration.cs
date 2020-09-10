using System.Collections.Generic;
using System.Linq;

namespace Configurateur
{
    public class Configuration
    {
        public List<IEvent> Events { get; }

        public static Configuration ConfigurationInitialize(List<IEvent> events)
        {
            return new Configuration(events);
        }

        private Configuration(List<IEvent> events)
        {
            Events = events;
        }

        public ModeleId ModdelSelectionneId { get; set; }

        public List<IEvent> SelectionneModele(ModeleId modeleId)
        {
            var events = new List<IEvent>();
            var modeleSelectionneEvent = new ModeleSelectionneEvent();
            modeleSelectionneEvent.Options = new Options[]
            {
                new Options(){IsSelectionnee =true,
                OptionId = new OptionId("A")},

                new Options(){IsSelectionnee =false,
                OptionId = new OptionId("B")},
            };

            var optionSelectionneeEvent = new OptionSelectionneeEvent(new OptionId("A"));

            events.Add(modeleSelectionneEvent);
            events.Add(optionSelectionneeEvent);

            return events;
        }

        public List<IEvent> SelectionneOption(OptionId optionId)
        {
            var events = new List<IEvent>();

            var optSelectEvents = Events
                .Where(ev => ev.GetType().IsAssignableFrom(typeof(OptionSelectionneeEvent)))
                .Cast<OptionSelectionneeEvent>();

            if (!optSelectEvents.Any(ev => ev.OptionSelectionneeId.Equals(optionId)))
            {
                var optionSelectionneeEvent = new OptionSelectionneeEvent(optionId);
                events.Add(optionSelectionneeEvent);
            }

            return events;
        }
    }
}