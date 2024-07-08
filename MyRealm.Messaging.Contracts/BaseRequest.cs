using System.ComponentModel.DataAnnotations;

namespace MyRealm.Messaging.Contracts
{
    public record BaseRequest
    {
        public void Validate()
        {
            var validationContext = new ValidationContext(this);
            Validator.ValidateObject(this, validationContext, validateAllProperties: true);
        }
    }
}
