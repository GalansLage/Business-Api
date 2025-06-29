

namespace Domain.DomainEvents.ProductTransactionEvents
{
    public class TransactionCanceled:IDomainEvent
    {
        public bool IsCancel { get; }

        public TransactionCanceled(bool isCancel)
        {
            IsCancel = isCancel;
        }
    }
}
