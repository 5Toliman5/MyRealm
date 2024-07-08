namespace MyReailm.Authentication.Domain.Entities
{
    public class SecurityToken
    {
        public string Value { get; private set; }
        public DateTime Expiry { get; private set; }
        public SecurityToken(string value, DateTime expiry)
        {
            if (expiry.Kind != DateTimeKind.Utc)
                throw new ArgumentException($"{nameof(expiry)} was not in the {nameof(DateTimeKind.Utc)} format.");
            Value = value;
            Expiry = expiry;
        }

    }
}
