using MyRealm.Domain.Common.Entities;

namespace MyRealm.Domain.Authentication.Entities
{
    public class SecurityToken
    {
        public string Value { get; private set; }
        public DateTime Expiry { get; private set; }
        public SecurityToken(string value, DateTime expiry)
        {
            if (expiry.Kind != DateTimeKind.Utc)
                throw new ArgumentException($"{nameof(expiry)} was not in the {nameof(DateTimeKind.Utc)} format.");
            this.Value = value;
            this.Expiry = expiry;
        }

    }
}
