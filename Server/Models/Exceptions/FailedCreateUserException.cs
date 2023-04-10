namespace Server.Models.Exceptions
{
    public class FailedCreateUserException : Exception
    {
        public FailedCreateUserException(string actor)
            : base(message: $"{actor}, he is Not Allowed In System ")
        { }
    }
}
