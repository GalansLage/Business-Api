
using Domain.Core.Enums;

namespace Domain.DomainEvents.ProductEvents
{
    class StateChanged:IDomainEvent
    {
        public PState State { get; }

        public StateChanged(PState state)
        {
            State = state;
        }
    }
}
