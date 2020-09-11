namespace Configurateur
{
    public struct OptionSelectionnee : IEvent
    {
        public OptionId OptionSelectionneeId { get; }

        public OptionSelectionnee(OptionId optionId)
        {
            OptionSelectionneeId = optionId;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} optionId : {OptionSelectionneeId}";
        }
    }
}