namespace Configurateur
{
    public struct ConfigurationEventWrapper : IEventWrapper
    {
        public ConfigurationId ConfigurationId { get; set; }

        public IEvent Event { get; set; }

        public string GetId()
        {
            return ConfigurationId.Id;
        }
    }
}