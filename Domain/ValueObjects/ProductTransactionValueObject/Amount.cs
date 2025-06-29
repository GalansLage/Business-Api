

namespace Domain.ValueObjects.ProductTransactionValueObject
{
    public class Amount : ValueObject
    {
        public int AmountVO { get; }

        protected Amount() { }
        public Amount(int amountVO)
        {
            if (amountVO < 0) throw new ArgumentException("La cantidad productos no puede ser negativa.");
            AmountVO = amountVO;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AmountVO;
        }

        public int Value()
        {
            return AmountVO;
        }
    }
}
