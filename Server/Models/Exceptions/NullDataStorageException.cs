namespace Server.Models.Exceptions
{
    public class NullDataStorageException : Exception
    {
        public NullDataStorageException(string paramaterName) : base(message: $"List OF {paramaterName} Is Empty ") { }
    }
}
