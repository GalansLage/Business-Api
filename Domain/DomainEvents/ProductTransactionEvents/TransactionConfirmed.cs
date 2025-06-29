

namespace Domain.DomainEvents.ProductTransactionEvents
{
    class TransactionConfirmed:IDomainEvent
    {
        public bool IsDeleted { get; }
        public DateTime DeletedTimeUtc { get; }

        public TransactionConfirmed(bool isDeleted, DateTime deletedTimeUtc)
        {
            IsDeleted = isDeleted;
            DeletedTimeUtc = deletedTimeUtc;
        }
    }
}
