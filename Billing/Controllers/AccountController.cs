using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Billing.Models.Service;
using Billing.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Billing.Controllers
{
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
		public IActionResult Forbidden()
		{
			return View();
		}
		
		[HttpGet]
		public async Task<IActionResult> Login()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return View();
		}
		
		[HttpPost]
		public async Task<IActionResult> Login(UserModel model)
		{
			ModelState.Remove("Contacts");
			ModelState.Remove("ConfirmPassword");
			if (ModelState.IsValid)
			{
				var user = await _userService.GetUserByUserModel(model);
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
		public async Task<IActionResult> RegisterClient(UserModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userService.GetUserByName(model.Name);
				if (user == null)
				{
					var create_task			= _userService.CreateUserWithUserModel(model, UserService.CLIENT_ROLE);
					var authenticate_task	= Authenticate(model.Name, UserService.CLIENT_ROLE);
					
					await Task.WhenAll(create_task, authenticate_task);
					
					return RedirectToAction("Index", "Client");
				}
				
				ModelState.AddModelError("GlobalError", "Пользователь с таким именем уже существует.");
			}
			return View(model);
		}
 
		private async Task Authenticate(string name, string role)
		{
			var claims_principal = _userService.GetClaimsPrincipal(name, role);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claims_principal);
		}
 
		public IActionResult Logout()
		{
			return RedirectToAction("Login", "Account");
		}
	}
}