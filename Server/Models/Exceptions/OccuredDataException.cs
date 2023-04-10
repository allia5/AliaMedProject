namespace Server.Models.Exceptions
{
    public class OccuredDataException : Exception
    {
        public OccuredDataException(string ParametreName) : base(message: $"{ParametreName} was Occured In DataBase ")
        {

        }
    }
}
