using System.Threading.Tasks;
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
		
		public async Task<IActionResult> Index()
		{
			var clients = await _clientService.Get();
			return View(clients);
		}
		
//		public async Task<IActionResult> Create()
	}
}
