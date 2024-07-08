using Microsoft.Data.SqlClient;
using MyRealm.Messaging.Domain.Entities.Domain;
using MyRealm.Messaging.Domain.Entities.Domain.Notifications;

namespace MyRealm.Messaging.DataAccess.Commands
{
    public static class CommandCreator
    {
        public static SqlCommand CreateEmailNotificationMessageInsertCommand(EmailNotificationMessageDomain message, SqlConnection connection)
        {
            var command = new SqlCommand(SqlCommands.InsertEmailMessage, connection);
            command.Parameters.AddRange(new[]
            {
                new SqlParameter("Message", message.Message),
                new SqlParameter("EmailAddress", message.EmailAddress),
                new SqlParameter("State", message.State)
            });
            return command;
        }
        public static SqlCommand CreateSmsNotificationMessageInsertCommand(SmsNotificationMessageDomain message, SqlConnection connection)
        {
            var command = new SqlCommand(SqlCommands.InsertSmsMessage, connection);
            command.Parameters.AddRange(new[]
            {
                new SqlParameter("Message", message.Message),
                new SqlParameter("PhoneNumber", message.PhoneNumber),
                new SqlParameter("State", message.State)
            });
            return command;
        }
        public static SqlCommand CreateUpdateStateCommand(BaseDomainMessage message, string table, SqlConnection connection)
        {
            var query = string.Format(SqlCommands.UpdateMessageState, table);
            var command = new SqlCommand(query, connection);
            command.Parameters.AddRange(
            [
                new SqlParameter("Id", message.Id),
                new SqlParameter("State", message.State)
            ]);
            return command;
        }
    }
}
