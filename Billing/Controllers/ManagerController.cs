using System.Threading.Tasks;
using Billing.Models.DataModel;
using Billing.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Controllers
{
	[Authorize(Roles = AccountService.MANAGER)]
	public class ManagerController : Controller
	{
		private readonly IClientService _clientService;

		public ManagerController(IClientService client_service)
		{
			_clientService = client_service;
		}
		
		public async Task<IActionResult> Index()
		{
			var clients = await _clientService.Get();
			return View(clients);
		}

		public async Task<IActionResult> Create(Client client)
		{
			await _clientService.Create(client);
			var clients = await _clientService.Get();
			return View("Index", clients);
		}
		
		public async Task<IActionResult> Update(Client client)
		{
			await _clientService.Update(client);
			var clients = await _clientService.Get();
			return View("Index", clients);
		}
	}
}
