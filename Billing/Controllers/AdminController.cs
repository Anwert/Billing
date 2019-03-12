using Billing.Models.Service;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Controllers
{
	public class AdminController : Controller
	{
		private readonly IClientService _clientService;

		public AdminController(IClientService client_service)
		{
			_clientService = client_service;
		}

		public IActionResult Index()
		{
			var clients = _clientService.Get();
			return View(clients);
		}
	}
}
