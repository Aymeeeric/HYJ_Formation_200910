using System.Collections.Generic;
using System.Linq;

namespace Configurateur
{
    public class Configuration
    {
        private readonly DecisionProjection _projection = new DecisionProjection();

        public Configuration(List<IEvent> history)
        {
            foreach (var @event in history)
            {
                _projection.Apply(@event);
            }
        }

        public List<IEvent> SelectionneModele()
        {
            var modeleSelectionneEvent = new ModeleSelectionne()
            {
                Options = new Options[]
                {
                    new Options(){
                        IsSelectionnee = true,
                        OptionId = new OptionId("A")},

                    new Options(){
                        IsSelectionnee = false,
                        OptionId = new OptionId("B")},
                }
            };

            var optionSelectionneeEvent = new OptionSelectionnee(new OptionId("A"));
            return new List<IEvent>() { modeleSelectionneEvent, optionSelectionneeEvent };

            // TODO : apply events to _projection
        }

        public List<IEvent> SelectionneOption(OptionId optionId)
        {
            if (_projection.OptionSelectionneeIds.Any(opt => opt.Equals(optionId)))
                return new List<IEvent>();

            return new List<IEvent>() { new OptionSelectionnee(optionId) };

            // TODO : apply events to _projection
        }

        public List<IEvent> DeselectionneOption(OptionId optionId)
        {
            if (_projection.OptionSelectionneeIds.Any(opt => opt.Equals(optionId)))
                return new List<IEvent>() { new OptionDeselectionnee(optionId) };

            return new List<IEvent>();

            // TODO : apply events to _projection
        }

        private class DecisionProjection
        {
            public List<OptionId> OptionSelectionneeIds { get; private set; } = new List<OptionId>();

            public void Apply(IEvent @event)
            {
                if (@event is OptionSelectionnee optEvt)
                    PlayOptionSelectionneeEvent(optEvt);

                if (@event is OptionDeselectionnee optDesEvt)
                    PlayOptionDeselectionneeEvent(optDesEvt);
            }

            private void PlayOptionSelectionneeEvent(OptionSelectionnee @event)
            {
                if (OptionSelectionneeIds != null &&
                    OptionSelectionneeIds.Any(id => id.Equals(@event)))
                    return;

                OptionSelectionneeIds.Add(@event.OptionSelectionneeId);
            }

            private void PlayOptionDeselectionneeEvent(OptionDeselectionnee @event)
            {
                if (OptionSelectionneeIds != null &&
                    OptionSelectionneeIds.Any(id => id.Equals(@event)))
                    OptionSelectionneeIds.Remove(@event.OptionDeselectionneeId);
            }
        }
    }
}