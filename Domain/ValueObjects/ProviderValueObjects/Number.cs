

namespace Domain.ValueObjects.ProviderValueObjects
{
    public class Number : ValueObject
    {
        public string NumberVO { get; }
        protected Number() { }
        public Number(string numberVO)
        {
            if (numberVO == null) throw new ArgumentNullException("El CI no puede ser nulo");
            if (numberVO.Length<8||numberVO.Length>10) throw new ArgumentException("EL CI debe tener entre 8 y 10 digitos");
            NumberVO = numberVO;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return NumberVO;
        }
    }
}
