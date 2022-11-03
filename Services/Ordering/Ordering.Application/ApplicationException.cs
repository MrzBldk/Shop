namespace Ordering.Application
{
    public class ApplicationException : Exception
    {
        internal ApplicationException(string message) : base(message) { }
    }
}
