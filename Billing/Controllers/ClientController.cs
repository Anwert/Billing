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
			var client_id	= _userService.GetUserByName(User.Identity.Name).Id;
			var contracts	= await _contractService.GetContractsForClient(client_id);
			return View(contracts);
		}
	}
}