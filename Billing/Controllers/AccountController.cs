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
		private readonly IUserService _userService;
		
		public AccountController(IUserService user_service)
		{
			_userService = user_service;
		}
		
		public IActionResult Index()
		{
			if (User.IsInRole(UserService.CLIENT_ROLE))
			{
				return RedirectToAction("Index", "Client");
			}

			if (User.IsInRole(UserService.MANAGER_ROLE))
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
				var user = await _userService.GetUserByLoginModel(model);
				if (user != null)
				{
					await Authenticate(model.Name, user.Role); // аутентификация

					return user.Role == UserService.MANAGER_ROLE
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
				var user = await _userService.GetUserByName(model.Name);
				if (user == null)
				{
					var create_task			= _userService.Create(model, UserService.CLIENT_ROLE);
					var authenticate_task	= Authenticate(model.Name, UserService.CLIENT_ROLE);
					
					await Task.WhenAll(create_task, authenticate_task);
					
					return RedirectToAction("Index", "Client");
				}
				
				ModelState.AddModelError("GlobalError", "Пользователь с именем уже существует.");
			}
			return View(model);
		}
 
		private async Task Authenticate(string name, string role)
		{
			var claims_principal = _userService.GetClaimsPrincipal(name, role);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claims_principal);
		}
 
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Account");
		}
	}
}