
namespace Domain.ValueObjects.ProductValueObjects
{
    public class StockStore : ValueObject
    {
        public int StockStoreVO { get; }
        protected StockStore() { }
        public StockStore(int stokeStoreVO)
        {
            if (stokeStoreVO == null)
                StockStoreVO = 0;

            StockStoreVO = stokeStoreVO;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StockStoreVO;
        }
    }
}
