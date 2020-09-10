using System.Linq;

namespace Configurateur
{
    public struct ModeleSelectionneEvent : IEvent
    {
        public Options[] Options { get; set; }

        public override string ToString()
        {
            return $"{this.GetType().Name} options : {string.Join(",", Options.Select(opt => opt.OptionId))}";
        }

        public override bool Equals(object obj)
        {
            return this.Options.Length == ((ModeleSelectionneEvent)obj).Options.Length;
        }
    }
}