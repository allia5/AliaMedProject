namespace Server.Models.Exceptions
{
    public class StatusValidationException : Exception
    {
        public StatusValidationException(string parametre) : base($"{parametre} Status Is Invalide")
        {

        }
    }
}
