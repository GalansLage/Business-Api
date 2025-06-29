
using Domain.ValueObjects.MoneyValueObjects;

namespace Domain.ValueObjects.ProductValueObjects
{
    public class Price : ValueObject
    {
        public Money PriceVO { get; }

        protected Price() { }
        public Price(Money price)
        {
            PriceVO = price ?? throw new ArgumentNullException("El Precio no puede ser nulo");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PriceVO;
        }

        public Money Value()
        {
            return PriceVO;
        }
    }
}
