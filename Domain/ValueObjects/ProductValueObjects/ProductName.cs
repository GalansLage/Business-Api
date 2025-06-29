

namespace Domain.ValueObjects.ProductValueObjects
{
    public class ProductName : ValueObject
    {
        public string Name { get; }

        protected ProductName() { }
        public ProductName(string name)
        {
            if (name == null) throw new ArgumentNullException("EL nombre no puede ser nullo");
            if (name.Length < 4 || name.Length > 35) throw new ArgumentException("El nombre debe de tener de 4-35 caracteres");

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
