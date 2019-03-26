using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Controllers
{
	[Authorize(Roles = AccountController.CLIENT)]
	public class ClientController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}