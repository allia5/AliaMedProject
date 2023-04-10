namespace Client.Services.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message) : base(message: "User Not Have A Permission in this Ressource ")
        {

        }
    }
}
