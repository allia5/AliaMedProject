namespace Server.Models.Exceptions
{
    public class IdentityTokenException : Exception
    {
        public IdentityTokenException(string ParametreName) : base(message: $"Your {ParametreName} Is Invalide ") { }

    }
}
