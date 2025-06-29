

using Domain.ValueObjects.MoneyValueObjects;

namespace Domain.ValueObjects.ProductValueObjects
{
    public class Cost : ValueObject
    {
        public Money CostVO { get; }

        protected Cost() { }
        public Cost(Money costVO)
        {
            CostVO = costVO ?? throw new ArgumentNullException("EL costo no puede ser nulo");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CostVO;
        }

        public Money Value()
        {
            return CostVO;
        }
    }
}
