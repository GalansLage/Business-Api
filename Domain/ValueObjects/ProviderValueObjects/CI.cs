


namespace Domain.ValueObjects.ProviderValueObjects
{
    public class CI : ValueObject
    {
        public string CIVO { get; }

        protected CI() { }
        public CI(string cIVO)
        {
            if (cIVO == null) throw new ArgumentNullException("El CI no puede ser nulo");
            if (cIVO.Length != 11) throw new ArgumentException("EL CI debe tener un total de once números");
            CIVO = cIVO;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CIVO;
        }
    }
}
