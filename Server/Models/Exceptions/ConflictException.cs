namespace Server.Models.Exceptions
{
    public class ConflictException : Exception
    {


        public ConflictException(Exception innerException)
            : base(message: "Conflict Data input, contact support.", innerException) { }


    }
}
