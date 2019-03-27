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
		private readonly IUserService _userService;

		public ManagerController(IUserService user_service)
		{
			_userService = user_service;
		}
		
		public async Task<IActionResult> Index()
		{
			var clients = await _userService.GetClients();
			return View(clients);
		}

		public async Task<IActionResult> Create(User client)
		{
			await _userService.Create(client);
			var clients = await _userService.GetClients();
			return View("Index", clients);
		}
		
		public async Task<IActionResult> Update(User client)
		{
			await _userService.Update(client);
			var clients = await _userService.GetClients();
			return View("Index", clients);
		}
	}
}
