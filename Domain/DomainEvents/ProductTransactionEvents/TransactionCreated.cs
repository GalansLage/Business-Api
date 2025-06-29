

using Domain.ValueObjects.ProductTransactionValueObject;
using Domain.ValueObjects.ProductValueObjects;

namespace Domain.DomainEvents.ProductTransactionEvents
{
    public class TransactionCreated:IDomainEvent
    {
        public Amount Amount { get; }
        public InTime Date { get;  }

        public TransactionCreated(Amount amount, InTime date)
        {
            Amount = amount;
            Date = date;
        }
    }
}
