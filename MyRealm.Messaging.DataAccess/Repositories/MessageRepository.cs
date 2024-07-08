using Microsoft.Data.SqlClient;
using MyRealm.Common.Repositories;
using MyRealm.Messaging.DataAccess.Commands;
using MyRealm.Messaging.Domain.Entities.Domain;
using MyRealm.Messaging.Domain.Repositories;

namespace MyRealm.Messaging.DataAccess.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        public string ConnectionString { get; }
        public MessageRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        public async Task<BaseDomainMessage> Insert(BaseDomainMessage message)
        {
            try
            {
                await using var connection = new SqlConnection(this.ConnectionString);
                connection.Open();
                var command = CommandFactory.CreateInsertCommand(message, connection);
                var insertedId = Convert.ToInt32(await command.ExecuteScalarAsync());
                message.Id = insertedId;
                return message;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task UpdateState(BaseDomainMessage message)
        {
            try
            {
                await using var connection = new SqlConnection(this.ConnectionString);
                connection.Open();
                var command = CommandFactory.CreateUpdateStateCommand(message, connection);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
