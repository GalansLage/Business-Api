using Domain.Aggregates;
using Domain.Core.Enums;
using Domain.Core.Utils;
using Domain.DomainEvents;
using Domain.DomainEvents.ProductEvents;
using Domain.Entities.ProductTransactionEntity;
using Domain.Entities.ProviderEntity;
using Domain.ValueObjects.MoneyValueObjects;
using Domain.ValueObjects.ProductValueObjects;

namespace Domain.Entities.ProductEntity
{
    public class Product:Entity<int>,IAggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        private IReadOnlyList<IDomainEvent> domainEvents => _domainEvents.AsReadOnly(); 
        public ProductName ProductName { get; private set; } 
        public Price Price { get; private set; }
        public Cost Cost { get; private set; } 
        public int ProviderId { get; private set; }
        public Provider Provider { get; private set; }
        public Category Category { get; private set; }


        protected Product() { }
        public Product(
            string productName,
            Money price,
            Money cost,
            int providerId,
            string category)
        {
            ProductName = new ProductName(ToTitleCase.TitleCase(productName));
            Price = new Price(price);
            Cost = new Cost(cost);
            ProviderId = providerId;
            Category = new Category(ToTitleCase.TitleCase(category));

            _domainEvents.Add(new ProductCreated(ProductName, Price, Cost, Category));
        }
        //Product UseCases

        public bool Update(string productName,
            decimal amountPrice,
            decimal amountCost,
            CurrencyEnum currencyEnum,
            Provider provider,
            string category)
        {
            ProductName = new ProductName(productName);
            Price = new Price(new Money(amountPrice,currencyEnum));
            Cost = new Cost(new Money(amountCost, currencyEnum));
            Provider = provider;
            Category = new Category(category);

            return true;
        }
        public decimal CalculateIncome()
        {
            var income = Price.Value() - Cost.Value();
            return income.Cash;
        }
        
        public bool PriceChange(int amount, CurrencyEnum currencyEnum)
        {
            Price =new Price(new Money(amount, currencyEnum));
            return true;
        }
        public bool CategoryChange(string category)
        {
            Category = new Category(category);
            return true;
        }
    
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
