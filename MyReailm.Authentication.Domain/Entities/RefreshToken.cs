using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyRealm.Authentication.Domain.Entities
{
	[Table("refresh_tokens")]
	public class RefreshToken
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

		public RefreshToken(string value, DateTime expiresAt)
		{
			Value = value;
			ExpiresAt = expiresAt;
			CreatedAt = DateTime.UtcNow;
		}

		private RefreshToken()
		{
			
		}
	}
}
