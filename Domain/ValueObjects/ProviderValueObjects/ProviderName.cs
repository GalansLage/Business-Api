

namespace Domain.ValueObjects.ProviderValueObjects
{
    public class ProviderName : ValueObject
    {
        public string Name { get; }

        protected ProviderName() { }
        public ProviderName(string name)
        {
            if (name == null) throw new ArgumentNullException("EL nombre no puede ser nullo");
            if (name.Length < 3 || name.Length > 10) throw new ArgumentException("El nombre debe de tener de 3-10 caracteres");
            Name = name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }

        public string Value()
        {
            return Name;
        }
    }
}
