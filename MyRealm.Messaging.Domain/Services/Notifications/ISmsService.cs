using MyRealm.Messaging.Domain.Entities.Domain.Notifications;

namespace MyRealm.Messaging.Domain.Services.Notifications
{
    public interface ISmsService
    {
        Task<bool> SendSms(SmsNotificationMessageDomain smsNotification);
    }
}
