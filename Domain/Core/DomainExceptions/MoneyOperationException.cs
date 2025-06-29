using System.Runtime.Serialization;

namespace Domain.Core.DomainExceptions
{
    class MoneyOperationException:Exception
    {
        public MoneyOperationException() : base() { }

        public MoneyOperationException(string? message) : base(message)
        {
        }
        public MoneyOperationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MoneyOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
