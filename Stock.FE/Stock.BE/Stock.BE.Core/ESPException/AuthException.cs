namespace Stock.BE.Core.ESPException
{
    public class AuthException : Exception
    {
        public AuthException(string message) : base (message) { }
    }
}
