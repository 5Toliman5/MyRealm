namespace MyRealm.Authentication.Infrastructure.Exceptions
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException(string message) : base(message) { }
        public WrongPasswordException() { }
    }
}
