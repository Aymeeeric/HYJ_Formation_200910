namespace Configurateur
{
    public struct OptionId
    {
        public OptionId(string optionId)
        {
            this.Id = optionId;
        }

        public string Id { get; set; }
    }
}