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
            var modeleSelectionneEvent = new ModeleSelectionneEvent()
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

            var optionSelectionneeEvent = new OptionSelectionneeEvent(new OptionId("A"));
            return new List<IEvent>() { modeleSelectionneEvent, optionSelectionneeEvent };
        }

        public List<IEvent> SelectionneOption(OptionId optionId)
        {
            if (_projection.OptionSelectionneeIds.Any(opt => opt.Equals(optionId)))
                return new List<IEvent>();
            else
                return new List<IEvent>() { new OptionSelectionneeEvent(optionId) };
        }

        private class DecisionProjection
        {
            public List<OptionId> OptionSelectionneeIds { get; private set; } = new List<OptionId>();

            public void Apply(IEvent @event)
            {
                if (@event is ModeleSelectionneEvent modEvt)
                    PlayModeleSelectionneEvent(modEvt);

                if (@event is OptionSelectionneeEvent optEvt)
                    PlayOptionSelectionneeEvent(optEvt);
            }

            private void PlayModeleSelectionneEvent(ModeleSelectionneEvent @event)
            {
                OptionSelectionneeIds = @event.Options
                    .Where(opt => opt.IsSelectionnee == true)
                    .Select(opt => opt.OptionId).ToList();
            }

            private void PlayOptionSelectionneeEvent(OptionSelectionneeEvent @event)
            {
                if (OptionSelectionneeIds != null &&
                    OptionSelectionneeIds.Any(id => id.Equals(@event)))
                    return;

                OptionSelectionneeIds.Add(@event.OptionSelectionneeId);
            }
        }
    }
}