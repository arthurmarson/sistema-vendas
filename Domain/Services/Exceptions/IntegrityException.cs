namespace SalesWebMvc.Services.Domain.Exceptions
{
    public class IntegrityException : ApplicationException
    {
        public IntegrityException(string? message) : base(message)
        {
        }
    }
}
