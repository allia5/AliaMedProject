namespace Client.Services.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message: "Resource is Already Existing In System ")
        {

        }
    }
}
