using Configurateur;
using Configurateur.Services;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ConfigurateurTest
{
    public class EventRaisedTest
    {
        [Fact]
        public void Config_Should_Raise_Modele_Selectionne_And_OptionA_Selectionne_When_Modele_Selectionne()
        {
            Configuration config = new Configuration(new List<IEvent>());

            var modeleSelectionneEvent = new ModeleSelectionne(new ModeleId("MODELEA"), new Options[]
            {
                new Options()
                {
                    IsSelectionnee = true,
                    OptionId = new OptionId("A")
                },

                new Options()
                {
                    IsSelectionnee = false,
                    OptionId = new OptionId("B")
                },
            });
            var optionSelectionneeEvent = new OptionSelectionnee(new OptionId("A"));

            List<IEvent> eventRaised = config.SelectionneModele();

            eventRaised.ShouldNotBeNull();
            eventRaised.Count.ShouldBe(2);

            eventRaised[0]
                .ShouldBeOfType<ModeleSelectionne>()
                .ShouldBe(modeleSelectionneEvent);

            eventRaised[1]
                .ShouldBeOfType<OptionSelectionnee>()
                .ShouldBe(optionSelectionneeEvent);
        }

        [Fact]
        public void Config_Should_Raise_OptionA_Selectionne_When_OptionB_Selectionne()
        {
            var optionId = new OptionId("B");

            var modeleSelectionneEvent = new ModeleSelectionne(new ModeleId("MODELEA"), new Options[]
            {
                new Options()
                {
                    IsSelectionnee = true,
                    OptionId = new OptionId("A")
                },

                new Options()
                {
                    IsSelectionnee = false,
                    OptionId = new OptionId("B")
                },
            });
            var optionSelectionneeEvent = new OptionSelectionnee(optionId);

            var history = new List<IEvent>() { modeleSelectionneEvent };
            Configuration config = new Configuration(history);

            List<IEvent> eventRaised = config.SelectionneOption(optionId);

            eventRaised.ShouldNotBeNull();
            eventRaised.Count.ShouldBe(1);

            eventRaised[0]
                .ShouldBeOfType<OptionSelectionnee>()
                .ShouldBe(optionSelectionneeEvent);
        }

        [Fact]
        public void Config_With_OptionA_Already_Selectionnee_Should_Raise_Nothing_When_OptionA_Selectionne()
        {
            var optionId = new OptionId("A");

            var modeleSelectionneEvent = new ModeleSelectionne(new ModeleId("MODELEA"), new Options[]
            {
                new Options()
                {
                    IsSelectionnee = true,
                    OptionId = new OptionId("A")
                },

                new Options()
                {
                    IsSelectionnee = false,
                    OptionId = new OptionId("B")
                },
            });
            var optionSelectionneeEvent = new OptionSelectionnee(optionId);

            var history = new List<IEvent>() { modeleSelectionneEvent, optionSelectionneeEvent };
            Configuration config = new Configuration(history);

            List<IEvent> eventRaised = config.SelectionneOption(optionId);

            eventRaised.ShouldBeEmpty();
        }

        [Fact]
        public void Config_Should_Raise_OptionA_Deselectionne_When_OptionA_Selectionne_Then_Deslectionne()
        {
            var optionId = new OptionId("A");

            var modeleSelectionneEvent = new ModeleSelectionne(new ModeleId("MODELEA"), new Options[]
            {
                new Options()
                {
                    IsSelectionnee = true,
                    OptionId = new OptionId("A")
                },

                new Options()
                {
                    IsSelectionnee = false,
                    OptionId = new OptionId("B")
                },
            });
            var optionSelectionneeEvent = new OptionSelectionnee(optionId);

            var optionDeselectionneeEvent = new OptionDeselectionnee(optionId);

            var history = new List<IEvent>() { modeleSelectionneEvent, optionSelectionneeEvent };
            Configuration config = new Configuration(history);

            List<IEvent> eventRaised = config.DeselectionneOption(optionId);

            eventRaised.ShouldNotBeNull();
            eventRaised.Count.ShouldBe(1);

            eventRaised[0]
                .ShouldBeOfType<OptionDeselectionnee>()
                .ShouldBe(optionDeselectionneeEvent);
        }

        [Fact]
        public void Config_Should_Raise_Nothing_When_OptionB_Deselectionne()
        {
            var optionId = new OptionId("A");

            var modeleSelectionneEvent = new ModeleSelectionne(new ModeleId("MODELEA"), new Options[]
            {
                new Options()
                {
                    IsSelectionnee = true,
                    OptionId = new OptionId("A")
                },

                new Options()
                {
                    IsSelectionnee = false,
                    OptionId = new OptionId("B")
                },
            });
            var optionSelectionneeEvent = new OptionSelectionnee(optionId);

            var history = new List<IEvent>() { modeleSelectionneEvent, optionSelectionneeEvent };
            Configuration config = new Configuration(history);

            List<IEvent> eventRaised = config.DeselectionneOption(new OptionId("B"));

            eventRaised.ShouldBeEmpty();
        }

        [Fact]
        public void Should_Show_All_OnGoing_Config()
        {
            var modeleSelectionneEvent = new ModeleSelectionne(new ModeleId("MODELEA"), new Options[]
            {
                new Options()
                {
                    IsSelectionnee = true,
                    OptionId = new OptionId("A")
                },

                new Options()
                {
                    IsSelectionnee = false,
                    OptionId = new OptionId("B")
                },
            });

            var wrapper = new ConfigurationEventWrapper()
            {
                ConfigurationId = new ConfigurationId("CONFIGA"),
                Event = modeleSelectionneEvent
            };

            ConfigEnAttenteProjection projection = new ConfigEnAttenteProjection();
            projection.Apply(wrapper);

            projection.Configs.ShouldNotBeEmpty();
            projection.Configs.Count.ShouldBe(1);
        }

        [Fact]
        public void Should_Store_Events_When_Publish_Event()
        {
            var modeleSelectionneEvent = new ModeleSelectionne(new ModeleId("MODELEA"), new Options[]
            {
                new Options()
                {
                    IsSelectionnee = true,
                    OptionId = new OptionId("A")
                },

                new Options()
                {
                    IsSelectionnee = false,
                    OptionId = new OptionId("B")
                },
            });

            var wrapper = new ConfigurationEventWrapper()
            {
                ConfigurationId = new ConfigurationId("CONFIGA"),
                Event = modeleSelectionneEvent
            };
            var eventStore = new InMemoryEventStore();
            var service = new PubSubService(eventStore, new List<IProjection>());

            service.Handle(new List<IEventWrapper> { wrapper });

            eventStore.Events.ShouldNotBeEmpty();
            eventStore.Events.Count.ShouldBe(1);
        }

        [Fact]
        public void Should_Call_Handlers_When_Publish_Event()
        {
            var modeleSelectionneEvent = new ModeleSelectionne(new ModeleId("MODELEA"), new Options[]
            {
                new Options()
                {
                    IsSelectionnee = true,
                    OptionId = new OptionId("A")
                },

                new Options()
                {
                    IsSelectionnee = false,
                    OptionId = new OptionId("B")
                },
            });

            var wrapper = new ConfigurationEventWrapper()
            {
                ConfigurationId = new ConfigurationId("CONFIGA"),
                Event = modeleSelectionneEvent
            };
            var eventStore = new InMemoryEventStore();
            var projection = new ConfigEnAttenteProjection();
            var service = new PubSubService(eventStore, new List<IProjection> { projection });

            service.Handle(new List<IEventWrapper> { wrapper });

            eventStore.Events.ShouldNotBeEmpty();
            eventStore.Events.Count.ShouldBe(1);

            projection.Configs.ShouldNotBeEmpty();
            projection.Configs.Count.ShouldBe(1);
        }

        [Fact]
        public void Should_Display_Updated_Projections_When_Send_Command()
        {
            var eventStore = new InMemoryEventStore();
            var projection = new ConfigEnAttenteProjection();
            var pubSubService = new PubSubService(eventStore, new List<IProjection> { projection });

            var aggregate = new Configuration(new List<IEvent>());
            var events = aggregate.SelectionneModele();

            pubSubService.Handle(events.Select(
                evt => (IEventWrapper)new ConfigurationEventWrapper()
                {
                    ConfigurationId = new ConfigurationId("CONFIGA"),
                    Event = evt
                }
                    )
                .ToList()
            );

            eventStore.Events.ShouldNotBeEmpty();
            eventStore.Events.Count.ShouldBe(2);

            projection.Configs.ShouldNotBeEmpty();
            projection.Configs.Count.ShouldBe(1);
        }

        [Fact]
        public void Should_Display_Updated_Projections_When_Send_Command_Via_Services()
        {
            var eventStore = new InMemoryEventStore();
            var projection = new ConfigEnAttenteProjection();
            var pubSubService = new PubSubService(eventStore, new List<IProjection> { projection });

            var configService = new ConfigurationService(eventStore, pubSubService);
            configService.SelectionneModele(new ConfigurationId("MODELE1"));

            eventStore.Events.ShouldNotBeEmpty();
            eventStore.Events.Count.ShouldBe(2);

            projection.Configs.ShouldNotBeEmpty();
            projection.Configs.Count.ShouldBe(1);

        }

        [Fact]
        public void Should_Not_Save_Event_If_Already_Recorded()
        {
            var eventStore = new EventStore();
            var projection = new ConfigEnAttenteProjection();
            var pubSubService = new PubSubService(eventStore, new List<IProjection> { projection });


        }
    }

}