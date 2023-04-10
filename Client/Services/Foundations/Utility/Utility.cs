namespace Client.Services.Foundations.Utility
{
    public static class Utility
    {
        public static string ConvertToken(this string Token)
        {
            return Token.Replace('-', '/');
        }
    }
}
