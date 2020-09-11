using System;

namespace Configurateur
{
    public struct Options
    {
        public OptionId OptionId { get; set; }
        public bool IsSelectionnee { get; set; }
    }

    public struct PublishedEvent
    {
        public PublishedEventId PublishedEventId { get; set; }
        public IEvent Event { get; set; }
    }

    public class PublishedEventId
    {
        public Guid Id { get; set; }
    }
}