

using Domain.ValueObjects.ProductValueObjects;

namespace Domain.DomainEvents.ProductEvents
{
    public class ProductCreated:IDomainEvent
    {
        public ProductName ProductName { get; }
        public Price Price { get; }
        public Cost Cost { get; }
        public Category Category { get; }

        public ProductCreated(
            ProductName productName,
            Price price,
            Cost cost,
            Category category)
        {
            ProductName = productName;
            Price = price;
            Cost = cost;
            Category = category;

        }

    }
}
