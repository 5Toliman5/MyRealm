using MyRealm.Common.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyRealm.Authentication.Domain.Entities
{
    [Table("users")]
	public class ApiUser : IIntIdEntity
    {
        [Column("id"), Key]
        public int Id { get; init; }

        [Column("user_name"), MaxLength(64)]
        public string UserName { get; private set; }

		[Column("password")]
		public string Password { get; private set; }

		[Column("email"), MaxLength(64)]
		public string Email { get; private set; }

		[Column("created_at")]
		public DateTime CreatedAt { get; init; }

		[Column("suspended")]
		public bool Suspended { get; set; }

		public AccessToken? AccessToken { get; set; }

		public RefreshToken? RefreshToken { get; set; }

		public ApiUser(string userName, string password, string email)
        {
            UserName = userName;
            Password = password;
			Email = email;
			CreatedAt = DateTime.UtcNow;
        }

        private ApiUser()
        {

        }
    }
}
