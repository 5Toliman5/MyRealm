namespace MyRealm.Authentication.Domain.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string message) : base(message) { }

        public UserAlreadyExistsException() { }
    }
}
