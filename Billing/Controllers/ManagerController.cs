using System.Threading.Tasks;
using Billing.Models.DataModel;
using Billing.Models.Repository;
using Billing.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Controllers
{
	[Authorize(Roles = UserService.MANAGER_ROLE)]
	public class ManagerController : Controller
	{
		private readonly IUserService _userService;
		
		private readonly IContractService _contractService;

		public ManagerController(IUserService user_service,
			IContractService contract_service)
		{
			_userService = user_service;
			_contractService = contract_service;
		}
		
		public async Task<IActionResult> Index()
		{
			var contracts = await _contractService.GetContracts();
			return View(contracts);
		}

//		public async Task<IActionResult> Create(User client)
//		{
//			await _userService.Create(client);
//			var clients = await _userService.GetClients();
//			return View("Index", clients);
//		}
//		
//		public async Task<IActionResult> Update(User client)
//		{
//			await _userService.Update(client);
//			var clients = await _userService.GetClients();
//			return View("Index", clients);
//		}
	}
}
