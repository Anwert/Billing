using System;
using System.Threading.Tasks;
using Billing.Models.DataModel;
using Billing.Models.Service;
using Billing.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Controllers
{
	[Authorize(Roles = UserService.MANAGER_ROLE)]
	public class ManagerController : Controller
	{
		private readonly IUserService		_userService;
		private readonly IContractService	_contractService;
		private readonly IStatusService		_statusService;
		private readonly IFavourService		_favourService;

		public ManagerController(IUserService user_service,
			IContractService contract_service,
			IStatusService status_service,
			IFavourService favour_service)
		{
			_userService		= user_service;
			_contractService	= contract_service;
			_statusService		= status_service;
			_favourService		= favour_service;
		}
		
		public async Task<IActionResult> Index()
		{
			var contracts	= await _contractService.GetContracts();
			var statuses	= await _statusService.GetStatuses();

			ViewBag.Statuses = statuses;
			
			return View(contracts);
		}
		
		[HttpGet]
		public async Task<IActionResult>CreateContract()
		{
			var favours		= await _favourService.GetFavours();
			var statuses	= await _statusService.GetStatuses();
			var clients		= await _userService.GetClients();
			
			ViewBag.Favours		= favours;
			ViewBag.Statuses	= statuses;
			ViewBag.Clients		= clients;
			
			return View();
		}

		[HttpPost]
		public async Task<IActionResult>CreateContract(Contract contract)
		{
			contract.Manager = await GetCurrentManager();
			await _contractService.Create(contract);
			
			return RedirectToAction("Index");
		}
		
		[HttpPost]
		public async Task<IActionResult> UpdateStatusForContract(int new_status_id, int contract_id)
		{
			try
			{
				await _contractService.UpdateStatusForContract(new_status_id, contract_id);
				
				return Json(new { error = false });
			}
			catch (Exception ex)
			{
				return Json(new { error = true, message = ex.Message });
			}
		}
		
		public async Task<IActionResult> Clients()
		{
			var clients = await _userService.GetClients();
			
			return View(clients);
		}
		
		[HttpGet]
		public IActionResult CreateClient()
		{
			return View();
		}
		
		[HttpPost]
		public async Task<IActionResult> CreateClient(ClientModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userService.GetUserByName(model.Name);
				if (user == null)
				{
					await _userService.CreateClient(model);
					
					return RedirectToAction("Clients", "Manager");
				}
				
				ModelState.AddModelError("GlobalError", "Пользователь с таким именем уже существует.");
			}
			
			return View(model);
		}
		
		private async Task<User> GetCurrentManager()
		{
			return await _userService.GetUserByName(User.Identity.Name);
		}
	}
}
