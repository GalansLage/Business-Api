
using Domain.Core.Enums;
using Domain.DomainEvents;
using Domain.DomainEvents.ProductTransactionEvents;
using Domain.Entities.ProductItemEntity;
using Domain.ValueObjects.ProductTransactionValueObject;
using Domain.ValueObjects.ProductValueObjects;

namespace Domain.Entities.ProductTransactionEntity
{
    public class ProductTransaction:Entity<int>
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        private IReadOnlyList<IDomainEvent> domainEvents => _domainEvents.AsReadOnly();
        public Amount Amount { get; private set; } 
        public InTime Date { get; private set; }
        public List<ProductItem> Products { get; private set; } = new();
        public bool IsCancel { get; private set; } = false;

        protected ProductTransaction() { }
        public ProductTransaction(List<ProductItem> productItems)
        {
            Amount = new Amount( Products.Count());
            Date = new InTime(DateTime.UtcNow);
            Products = productItems;
            _domainEvents.Add(new TransactionCreated(Amount, Date));
        }

        public decimal ConfirmTransaction()
        {
            IsDeletedChange();
            return TotalIncome();
        }

        public void CancelTransaction()
        {
            IsCancel = true;
            _domainEvents.Add(new TransactionCanceled(true));
        }

        public decimal TotalIncome()
        {
            decimal total = 0;
            foreach (var product in Products)
            {
                total += product.Product.CalculateIncome();
            }
            return total;
        }

        public bool AddProduct(ProductItem product)
        {
            if (product == null) throw new ArgumentNullException("El valor del producto que se esta agregando es nulo");

            if (product.ProductState.ToString().ToUpper().Equals("OUTSTORE"))
                throw new InvalidOperationException("El producto ya se encuentra en la transacción");
            if(product.ProductState.ToString().ToUpper().Equals("SELL"))
                throw new InvalidOperationException("El producto ya se vendió");

            var inStore = PState.OutStore;

            product.SateChange(inStore);
            Products.Add(product);

            Amount = new Amount(Amount.Value()+1);

            return true;
        }

        public bool DeleteProduct(int productId)
        {
            var product = Products.FirstOrDefault(p => p.Id == productId);

            if (product == null) throw new NullReferenceException("El producto que se intenta eliminar de la transaccion es nulo");

            product.SateChange(PState.InStore);
            Products.Remove(product);

            Amount = new Amount(Amount.Value() - 1);


            return true;
        }

        public void UpdateAmount()
        {
            Amount = new Amount(Products.Count());
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
