namespace Infrastructure.Core.InfrastructureExceptions
{
    public class ConflictException:Exception
    {
        public ConflictException(string message, Exception inner) : base(message, inner) { }
    }
}
