using Configurateur;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace ConfigurateurTest
{
    public class EventRaisedTest
    {
        [Fact]
        public void Config_Should_Raise_Modele_Selectionne_And_OptionA_Selectionne_When_Modele_Selectionne()
        {
            Configuration config = new Configuration(new List<IEvent>());

            var modeleId = new ModeleId("CONFIG01");
            var modelSelectionneEvent = new ModeleSelectionneEvent()
            {
                Options = new Options[]
                {
                    new Options(){IsSelectionnee =true,
                    OptionId = new OptionId("A")},

                    new Options(){IsSelectionnee =false,
                    OptionId = new OptionId("B")},
                }
            };
            var optionSelectionneeEvent = new OptionSelectionneeEvent(new OptionId("A"));

            List<IEvent> eventRaised = config.SelectionneModele(modeleId);

            eventRaised.ShouldNotBeNull();
            eventRaised.Count.ShouldBe(2);

            eventRaised[0]
                .ShouldBeOfType<ModeleSelectionneEvent>()
                .ShouldBe(modelSelectionneEvent);

            eventRaised[1]
                .ShouldBeOfType<OptionSelectionneeEvent>()
                .ShouldBe(optionSelectionneeEvent);
        }

        [Fact]
        public void Config_Should_Raise_OptionA_Selectionne_When_OptionB_Selectionne()
        {
            var optionId = new OptionId("B");

            var modelSelectionneEvent = new ModeleSelectionneEvent()
            {
                Options = new Options[]
                {
                    new Options(){IsSelectionnee =true,
                    OptionId = new OptionId("A")},

                    new Options(){IsSelectionnee =false,
                    OptionId = new OptionId("B")},
                }
            };
            var optionSelectionneeEvent = new OptionSelectionneeEvent(optionId);

            var history = new List<IEvent>() { modelSelectionneEvent };
            Configuration config = new Configuration(history);

            List<IEvent> eventRaised = config.SelectionneOption(optionId);

            eventRaised.ShouldNotBeNull();
            eventRaised.Count.ShouldBe(1);

            eventRaised[0]
                .ShouldBeOfType<OptionSelectionneeEvent>()
                .ShouldBe(optionSelectionneeEvent);
        }

        [Fact]
        public void Config_With_OptionA_Already_Selectionnee_Should_Raise_Nothing_When_OptionA_Selectionne()
        {
            var optionId = new OptionId("A");

            var modelSelectionneEvent = new ModeleSelectionneEvent()
            {
                Options = new Options[]
                {
                    new Options(){IsSelectionnee =true,
                    OptionId = new OptionId("A")},

                    new Options(){IsSelectionnee =false,
                    OptionId = new OptionId("B")},
                }
            };
            var optionSelectionneeEvent = new OptionSelectionneeEvent(optionId);

            var history = new List<IEvent>() { modelSelectionneEvent, optionSelectionneeEvent };
            Configuration config = new Configuration(history);

            List<IEvent> eventRaised = config.SelectionneOption(optionId);

            eventRaised.ShouldBeEmpty();
        }
    }
}