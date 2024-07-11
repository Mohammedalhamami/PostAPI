using Microsoft.AspNetCore.Identity;

namespace Posts.API.Repositories
{
	public interface ITokenRepository
	{
		string CreateJWTToken(IdentityUser user, List<string> roles);
	}
}