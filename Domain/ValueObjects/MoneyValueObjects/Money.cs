using Domain.Core.DomainExceptions;
using Domain.Core.Enums;
using Domain.Core.Utils;

namespace Domain.ValueObjects.MoneyValueObjects

{
    public class Money : ValueObject
    {
        public int Amaunt { get; }
        public Currency CurrencyVO { get; }

        protected Money() { }
        public Money(int amaunt, CurrencyEnum currencyEnum)
        {
            if (amaunt == null) throw new ArgumentNullException("El amount no puede ser nulo");

            if (amaunt < 0) throw new ArithmeticException("El valor de amount no puede ser menor que cero");

            Amaunt = amaunt;
            CurrencyVO = new Currency(currencyEnum);
        }
        public Money(decimal amaunt, CurrencyEnum currencyEnum)
        {
            if (amaunt == null) throw new ArgumentNullException("El amount no puede ser nulo");

            if (amaunt < 0) throw new ArithmeticException("El valor de amount no puede ser menor que cero");

            Amaunt = (int)(amaunt*100);
            CurrencyVO = new Currency(currencyEnum);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amaunt;
            yield return CurrencyVO;
        }

        public decimal Cash => (decimal)Amaunt / 100;
        public int Cents => Amaunt;

        public static Money operator +(Money a, Money b)
        {
            if (a.CurrencyVO != b.CurrencyVO) throw new MoneyOperationException("Las dos sumas de dinero son de divisas diferentes");

            CurrencyEnum currencyOperator;

            if(Enum.TryParse<CurrencyEnum>(a.CurrencyVO.Value(),true,out currencyOperator))
            {
                return new Money(a.Cents + b.Cents, currencyOperator);
            }
            else
            {
                throw new MoneyOperationException("No se pudo convertir el CurrencyEnum");
            }
        }

        public static Money operator -(Money a, Money b)
        {
            if (a.CurrencyVO != b.CurrencyVO) throw new MoneyOperationException("Las dos sumas de dinero son de divisas diferentes");

            CurrencyEnum currencyOperator;

            if (Enum.TryParse<CurrencyEnum>(a.CurrencyVO.Value(), true, out currencyOperator))
            {
                return new Money(a.Cents - b.Cents, currencyOperator);
            }
            else
            {
                throw new MoneyOperationException("No se pudo convertir el CurrencyEnum");
            }
        }

        public Money ChangeCurrency(Currency currency)
        {
            CurrencyValue currencyValue= new();
            CurrencyEnum newCurrency = new();
            int newAmount = 0;
            switch (currency.Value())
            {
                case "CUP":
                    if (CurrencyVO.Value().Equals(CurrencyEnum.CUP.ToString().ToUpper()))
                    {
                        newCurrency = CurrencyEnum.CUP;
                        newAmount = Amaunt;
                    }
                    else if (CurrencyVO.Value().Equals(CurrencyEnum.USD.ToString().ToUpper()))
                    {
                        newCurrency = CurrencyEnum.CUP;
                        newAmount = Amaunt / currencyValue.Usd;
                    }
                    else if (CurrencyVO.Value().Equals(CurrencyEnum.MLC.ToString().ToUpper()))
                    {
                        newCurrency = CurrencyEnum.CUP;
                        newAmount = Amaunt / currencyValue.Mlc;
                    }
                    break;
                case "USD":
                    if (CurrencyVO.Value().Equals(CurrencyEnum.USD.ToString().ToUpper()))
                    {
                        newCurrency = CurrencyEnum.USD;
                        newAmount = Amaunt;
                    }
                    else if (CurrencyVO.Value().Equals(CurrencyEnum.CUP.ToString().ToUpper()))
                    {
                        newCurrency = CurrencyEnum.USD;
                        newAmount = Amaunt * currencyValue.Usd;
                    }
                    else if (CurrencyVO.Value().Equals(CurrencyEnum.MLC.ToString().ToUpper()))
                    {
                        newCurrency = CurrencyEnum.USD;
                        newAmount = (Amaunt * currencyValue.Usd) / currencyValue.Mlc;
                    }
                    break;
                case "MLC":
                    if (CurrencyVO.Value().Equals(CurrencyEnum.MLC.ToString().ToUpper()))
                    {
                        newCurrency = CurrencyEnum.MLC;
                        newAmount = Amaunt;
                    }
                    else if (CurrencyVO.Value().Equals(CurrencyEnum.CUP.ToString().ToUpper()))
                    {
                        newCurrency = CurrencyEnum.MLC;
                        newAmount = Amaunt * currencyValue.Mlc;
                    }
                    else if (CurrencyVO.Value().Equals(CurrencyEnum.USD.ToString().ToUpper()))
                    {
                        newCurrency = CurrencyEnum.MLC;
                        newAmount = (Amaunt * currencyValue.Usd) / currencyValue.Mlc;
                    }
                    break;
                default:
                    throw new InvalidOperationException("La divisa seleccionada no existe");
            }
            return new Money(newAmount, newCurrency);
        }

        public string GetStingAmount()
        {
            return $"{Amaunt}{CurrencyVO.Value()}";
        }

    }
}
