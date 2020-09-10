namespace Configurateur
{
    public struct OptionSelectionneeEvent : IEvent
    {
        public OptionId OptionSelectionneeId { get; }

        public OptionSelectionneeEvent(OptionId optionId)
        {
            OptionSelectionneeId = optionId;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} optionId : {OptionSelectionneeId}";
        }
    }
}