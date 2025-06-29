

using Domain.Core.Utils;

namespace Domain.ValueObjects.ProductValueObjects
{
    public class InTime : ValueObject
    {
        public DateTime InTimeVO { get; }
        protected InTime() { }
        public InTime(DateTime inTimeVO)
        {
            if (inTimeVO == null) throw new ArgumentNullException("Fecha de entrada nula");

            if (inTimeVO.Kind == DateTimeKind.Utc)
            {
                InTimeVO = inTimeVO;
            }
            else
            {
                InTimeVO = inTimeVO.ToUniversalTime();
            }

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return InTimeVO;
        }
    }
}
