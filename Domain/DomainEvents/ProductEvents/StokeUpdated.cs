

using Domain.ValueObjects.ProductValueObjects;

namespace Domain.DomainEvents.ProductEvents
{
    public class StokeUpdated:IDomainEvent
    {
        public StockStore StockStore { get; }

        public StokeUpdated(int stockStore)
        {
            StockStore = new StockStore(stockStore);
        }
    }
}
