

using Domain.Entities.ProductTransactionEntity;

namespace Domain.DomainEvents.ProductTransactionEvents
{
    public record TransactionConfirmed(ProductTransaction ConfirmedTransaction):IDomainEvent;
    

}
