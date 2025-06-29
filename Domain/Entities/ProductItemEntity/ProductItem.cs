using Domain.Core.Enums;
using Domain.DomainEvents.ProductEvents;
using Domain.DomainEvents;
using Domain.ValueObjects.ProductValueObjects;
using Domain.Entities.ProductEntity;
using Domain.Entities.ProductTransactionEntity;

namespace Domain.Entities.ProductItemEntity
{
    public class ProductItem:Entity<int>
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        private IReadOnlyList<IDomainEvent> domainEvents => _domainEvents.AsReadOnly();
        public ProductCode ProductCode { get; private set; }
        public InTime InTime { get; private set; }
        public ProductState ProductState { get; private set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int? ProductTransactionId { get; set; }
        public ProductTransaction? ProductTransaction { get; set; }

        protected ProductItem() { }
        public ProductItem(string productCode, Product product)
        {
            ProductCode = new ProductCode(productCode);
            InTime = new InTime(DateTime.UtcNow);
            ProductState = new ProductState(PState.InStore);
            Product = product;
        }

        public bool SateChange(PState state)
        {
            ProductState = new ProductState(state);
            if (state.ToString().ToUpper().Equals("SELL"))
            {
                IsDeletedChange();
            }
            _domainEvents.Add(new StateChanged(state));

            return true;
        }
    }
}
