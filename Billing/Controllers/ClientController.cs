using Billing.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Controllers
{
	[Authorize(Roles = UserService.CLIENT_ROLE)]
	public class ClientController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}