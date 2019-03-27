using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Billing.Models.Service;
using Billing.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Billing.Controllers
{
	// TODO: в try catch все обернуть наверн по хорошему в контроллерах
	public class AccountController : Controller
	{
		private readonly IAccountService _accountService;
		
		public AccountController(IAccountService account_service)
		{
			_accountService = account_service;
		}
		
		public IActionResult Index()
		{
			if (User.IsInRole(AccountService.CLIENT))
			{
				return RedirectToAction("Index", "Client");
			}

			if (User.IsInRole(AccountService.MANAGER))
			{
				return RedirectToAction("Index", "Manager");
			}

			return RedirectToAction("Login", "Account");
		}
		
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		
		[HttpGet]
		public IActionResult Forbidden()
		{
			return View();
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _accountService.GetUser(model);
				if (user != null)
				{
					await Authenticate(model.Email, AccountService.MANAGER); // аутентификация

					return user.Role == AccountService.MANAGER
						? RedirectToAction("Index", "Manager")
						: RedirectToAction("Index", "Client");
				}
				
				ModelState.AddModelError("GlobalError", "Неверные имя пользователя или пароль.");
			}
			return View(model);
		}
		
		[HttpGet]
		public IActionResult RegisterClient()
		{
			return View();
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> RegisterClient(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _accountService.GetUserByEmail(model.Email);
				if (user == null)
				{
					await _accountService.AddUser(model);
 
					await Authenticate(model.Email, AccountService.CLIENT);
 
					return RedirectToAction("Index", "Client");
				}
				
				ModelState.AddModelError("GlobalError", "Пользователь уже существует.");
			}
			return View(model);
		}
 
		private async Task Authenticate(string email, string role)
		{
			var claims_principal = _accountService.GetClaimsPrincipal(email, role);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claims_principal);
		}
 
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Account");
		}
	}
}