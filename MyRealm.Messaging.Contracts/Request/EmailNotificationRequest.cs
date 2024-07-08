using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace MyRealm.Messaging.Contracts.Request
{
    [XmlRoot("EmailNotificationRequest")]
    public record EmailNotificationRequest : BaseRequest
    {
        [EmailAddress]
        [XmlElement("EmailAddress")]
        public required string EmailAddress { get; init; }
        [XmlElement("Message")]
        public required string Message { get; init; }
    }
}
