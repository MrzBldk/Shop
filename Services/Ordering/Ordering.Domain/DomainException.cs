namespace Ordering.Domain
{
    public class DomainException : Exception
    {
        internal DomainException(string businessMessage) : base(businessMessage) { }
    }
}