using Microsoft.AspNetCore.Mvc;
namespace TestAPI.Controllers
{
	[ApiController]
	public class ErrorController : ControllerBase
	{
		[Route("/error/test")]
		[HttpGet]
		public IActionResult Test()
		{
			throw new Exception("test");
		}
	}
}
