namespace Ordering.Infrastructure
{
    public class InfrastructureException : Exception
    {
        internal InfrastructureException(string message) : base(message) { }
    }
}
