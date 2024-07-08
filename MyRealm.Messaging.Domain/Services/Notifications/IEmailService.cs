using MyRealm.Messaging.Domain.Entities.Domain.Notifications;

namespace MyRealm.Messaging.Domain.Services.Notifications
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailNotificationMessageDomain emailNotification);
    }
}
