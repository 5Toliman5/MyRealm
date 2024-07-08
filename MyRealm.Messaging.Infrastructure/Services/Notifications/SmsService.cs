using MyRealm.Messaging.Domain.Entities.Domain.Notifications;
using MyRealm.Messaging.Domain.Services.Notifications;

namespace MyRealm.Messaging.Infrastructure.Services.Notifications
{
    public class SmsService : ISmsService
    {
        public async Task<bool> SendSms(SmsNotificationMessageDomain smsNotification)
        {
            return true;
        }
    }
}
