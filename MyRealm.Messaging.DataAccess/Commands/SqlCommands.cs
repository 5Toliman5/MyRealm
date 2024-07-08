namespace MyRealm.Messaging.DataAccess.Commands
{
    public static class SqlCommands
    {
        public const string InsertEmailMessage = "INSERT INTO EmailNotificationMessages(Message, State, EmailAddress) Values(@Message, @State, @EmailAddress); SELECT SCOPE_IDENTITY();";
        public const string InsertSmsMessage = "INSERT INTO SmsNotificationMessages(Message, State, PhoneNumber) Values(@Message, @State, @PhoneNumber); SELECT SCOPE_IDENTITY();";

        public const string UpdateMessageState = "UPDATE {0} SET State = @State WHERE Id = @Id";
    }
}
