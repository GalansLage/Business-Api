
using Domain.Core.Enums;

namespace Domain.ValueObjects.MoneyValueObjects
{
    public class Currency : ValueObject
    {
        public string CurrencyVO { get; }

        protected Currency() { }
        public Currency(CurrencyEnum currencyEnum)
        {
            CurrencyVO = currencyEnum.ToString().ToUpper() ?? throw new ArgumentNullException("El campo de divisa es obligatorio");
        }

        public Currency(string currencyVO)
        {
            CurrencyVO = currencyVO;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CurrencyVO;
        }

        public string Value()
        {
            return CurrencyVO;
        }
    }
}
