

namespace Domain.Core.Utils
{
    public class CurrencyValue
    {
        public int Usd { get; set; } = 375;
        public int Mlc { get; set; } = 260;

        public void CangeUsd(int usd)
        {
            Usd = usd;
        }
        public void CangeMlc(int mlc)
        {
            Mlc = mlc;
        }
    }
}
