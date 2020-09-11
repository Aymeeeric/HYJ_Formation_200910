namespace Configurateur
{
    public struct OptionDeselectionnee : IEvent
    {
        public OptionId OptionDeselectionneeId { get; }

        public OptionDeselectionnee(OptionId optionId)
        {
            OptionDeselectionneeId = optionId;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} optionId : {OptionDeselectionneeId}";
        }
    }
}