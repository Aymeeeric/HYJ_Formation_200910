using System.Linq;

namespace Configurateur
{
    public struct ModeleSelectionne : IEvent
    {
        public Options[] Options { get; set; }

        public override string ToString()
        {
            return $"{this.GetType().Name} options : {string.Join(",", Options.Select(opt => opt.OptionId))}";
        }

        public override bool Equals(object obj)
        {
            return this.Options.Length == ((ModeleSelectionne)obj).Options.Length;
        }

        public override int GetHashCode()
        {
            throw new System.NotImplementedException();
        }
    }
}