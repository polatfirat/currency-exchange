namespace CurrencyExchange.Domain.Exceptions
{
    public class ErrorResult
    {
        public int ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Source { get; set; }
    }
    public class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message)
        {

        }

        public DatabaseException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }

    public class OperationException : Exception
    {
        public OperationException(string message) : base(message)
        {

        }

        public OperationException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }

    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {

        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
