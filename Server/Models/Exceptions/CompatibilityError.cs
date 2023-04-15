namespace Server.Models.Exceptions
{
    public class CompatibilityError :Exception
    {
        public CompatibilityError(string ParametreNameOne, string ParametreNameTwo):base($"Data Of {ParametreNameOne} not compatible with {ParametreNameTwo}") { }
    }
}
