namespace Server.Models.Exceptions
{
    public class StorageValidationException : Exception
    {
        public StorageValidationException(Exception InnerException) : base(message: "Storage Validation Error, contact support.", InnerException)
        {

        }
    }
}
