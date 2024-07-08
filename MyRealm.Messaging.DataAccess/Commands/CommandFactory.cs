using Microsoft.Data.SqlClient;
using MyRealm.Messaging.Domain.Entities.Domain;
using MyRealm.Messaging.Domain.Entities.Domain.Notifications;

namespace MyRealm.Messaging.DataAccess.Commands
{
    public static class CommandFactory
    {
        private static Dictionary<Type, string> TableDictionary = new()
        {
            { typeof(EmailNotificationMessageDomain), "EmailNotificationMessages" },
            { typeof(SmsNotificationMessageDomain), "SmsNotificationMessages" },
        };
        public static SqlCommand CreateInsertCommand(BaseDomainMessage message, SqlConnection connection)
        {
            return message switch
            {
                EmailNotificationMessageDomain emailMessage => CommandCreator.CreateEmailNotificationMessageInsertCommand(emailMessage, connection),
                SmsNotificationMessageDomain smsMessage => CommandCreator.CreateSmsNotificationMessageInsertCommand(smsMessage, connection),
                _ => throw new ArgumentException("Cannot determine the DB table for this notification.")
            };
        }
        public static SqlCommand CreateUpdateStateCommand(BaseDomainMessage message, SqlConnection connection)
        {
            var type = message.GetType();
            if (TableDictionary.TryGetValue(type, out string tableName))
            {
                return CommandCreator.CreateUpdateStateCommand(message, tableName, connection);
            }
            throw new ArgumentException("Invalid message type");
        }
    }
}
