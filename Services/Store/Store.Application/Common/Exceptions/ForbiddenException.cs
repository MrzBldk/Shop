namespace Store.Application.Common.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException()
            : base()
        {
        }

        public ForbiddenException(string message)
            : base(message)
        {
        }

        public ForbiddenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ForbiddenException(string name, object key)
            : base($"You don't have acces to entity \"{name}\" ({key}).")
        {
        }
    }
}
