namespace Client.Services.Exceptions
{
    public class PreconditionFailedException :Exception
    {
        public PreconditionFailedException(string message) : base(message)
        {

        }
    }
}
