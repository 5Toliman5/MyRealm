using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace MyRealm.Authentication.Contracts.Request
{
    public record CreateApiUserRequest
    {
		[Length(1, 64)]
		public string UserName { get; init; }

		[Length(1, 64)]
		public string Password { get; init; }

		[Email, Length(1, 64)]
		public string Email { get; init; }
	}
}
