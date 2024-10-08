namespace MyRealm.Messaging.Infrastructure
{
    public class MessageConfiguration
    {
        public static string SectionName => nameof(MessageConfiguration);
        public int MaxRetriesNumber { get; set; }
    }
}
