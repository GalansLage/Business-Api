
using Domain.Core.Enums;

namespace Domain.ValueObjects.ProductValueObjects
{
    public class ProductState : ValueObject
    {
        public string State { get; }

        protected ProductState() { }
        public ProductState(PState state)
        {
            if (state == null) throw new ArgumentNullException("El estado es nulo");
            State = state.ToString().ToUpper();
        }

        public ProductState(string state)
        {
            State = state;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return State;
        }

        public string Value()
        {
            return State;
        }
    }
}
