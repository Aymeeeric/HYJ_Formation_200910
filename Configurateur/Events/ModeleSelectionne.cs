using System.Linq;

namespace Configurateur
{
    public struct ModeleSelectionne : IEvent
    {
        public ModeleSelectionne(ModeleId modeleId, Options[] options)
        {
            ModeleId = modeleId;
            Options = options;
        }

        public Options[] Options { get; }
        public ModeleId ModeleId { get; }

        public override string ToString()
        {
            return $"{this.GetType().Name} modeleID : {ModeleId.Id} options : {string.Join(",", Options.Select(opt => opt.OptionId))}";
        }

        public override bool Equals(object obj)
        {
            if (obj is ModeleSelectionne)
                return (this.Options.Length == ((ModeleSelectionne)obj).Options.Length &&
                        this.ModeleId.Id == ((ModeleSelectionne)obj).ModeleId.Id);

            return false;
        }

        public override int GetHashCode()
        {
            return ModeleId.GetHashCode() + Options.GetHashCode();
        }
    }
}