namespace MyRealm.Messaging.Infrastructure.Services.Notifications
{
    using MyRealm.Messaging.Domain.Entities.Domain.Notifications;
    using MyRealm.Messaging.Domain.Services.Notifications;
    using System.Threading.Tasks;

    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmail(EmailNotificationMessageDomain emailNotification)
        {
            return true;
        }
    }
}
