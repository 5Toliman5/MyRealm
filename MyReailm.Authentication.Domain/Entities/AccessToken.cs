using MyRealm.Common.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyRealm.Authentication.Domain.Entities
{
	[Table("access_tokens")]
	public class AccessToken : IIntIdEntity
    {
		[Column("id"), Key]
		public int Id { get; init; }

		[Column("user_id")]
		public int UserId { get; init; }

		[Column("value")]
		public string Value { get; init; }

		[Column("created_at")]
		public DateTime CreatedAt { get; init; }

		[Column("expires_at")]
		public DateTime ExpiresAt { get; init; }

		[Column("revoked")]
		public bool Revoked { get; set; }

		public AccessToken(string value, DateTime expiresAt)
        {
            Value = value;
			ExpiresAt = expiresAt;
			CreatedAt = DateTime.UtcNow;
		}

		private AccessToken()
		{
			
		}
	}
}
