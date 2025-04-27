using Microsoft.AspNetCore.Mvc;

namespace MyRealm.Authentication.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public abstract class AbstractController : ControllerBase
	{
	}
}
