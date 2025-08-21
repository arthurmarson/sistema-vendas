namespace SalesWebMvc.Services.Exceptions
{
    public class IntegrityException : ApplicationException
    {
        public IntegrityException(string? message) : base(message)
        {
        }

        public IntegrityException(string message, Microsoft.EntityFrameworkCore.DbUpdateException e) :base (message) 
        {
        }
    }
}
