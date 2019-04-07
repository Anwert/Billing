using System.Threading.Tasks;
using Billing.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Controllers
{
	[Authorize(Roles = UserService.CLIENT_ROLE)]
	public class ClientController : Controller
	{
		private readonly IContractService	_contractService;
		private readonly IUserService		_userService;

		public ClientController(IContractService contract_service, IUserService user_service)
		{
			_contractService	= contract_service;
			_userService		= user_service;
		}

		public async Task<IActionResult> Index()
		{
			var client		= await _userService.GetUserByName(User.Identity.Name);
			var contracts	= await _contractService.GetContractsForClient(client.Id);
			
			return View(contracts);
		}
		
		public async Task<IActionResult> GetManagers()
		{
			var managers = await _userService.GetManagers();
			
			return View(managers);
		}
		
		public async Task<IActionResult> GetManager(int id)
		{
			var manager = await _userService.GetUserById(id);
			
			if (manager.Role == UserService.MANAGER_ROLE)
			{
				return View(manager);
			}
			return RedirectToAction("Forbidden", "Account");
		}
		
		
	}
}