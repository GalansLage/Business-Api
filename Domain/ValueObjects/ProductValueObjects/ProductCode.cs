
namespace Domain.ValueObjects.ProductValueObjects
{
    public class ProductCode : ValueObject
    {
        public string Code { get; }

        protected ProductCode() { }
        public ProductCode(string poductCode)
        {
            if (poductCode == null) throw new ArgumentNullException("EL nombre no puede ser nullo");
            if (poductCode.Length < 4 || poductCode.Length > 25) throw new ArgumentException("El nombre debe de tener de 4-25 caracteres");
            Code = poductCode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
         public string Value()
        {
            return Code;
        }
    }
}
