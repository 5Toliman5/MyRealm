using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace MyRealm.Messaging.Contracts.Request
{
    [XmlRoot("SmsNotificationRequest")]
    public record SmsNotificationRequest : BaseRequest
    {
        [Phone]
        [XmlElement("PhoneNumber")]
        public required string PhoneNumber { get; init; }
        [XmlElement("Message")]
        public required string Message { get; init; }
    }
}
