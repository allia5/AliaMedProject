namespace Server.Models.Exceptions
{
    public class IdentityException : Exception
    {
        public IdentityException(Exception innerException)
          : base(message: "Has error In Identity.", innerException) { }
    }
}
