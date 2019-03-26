using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Billing.Models.Repository;
using Billing.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Billing.Controllers
{
	// TODO: переименовать Admin в Manager
	// TODO: в try catch все обернуть наверн по хорошему в контроллерах
	// TODO: возможно константы куда нибудь вынести
	// TODO: с рутами разобраться
	public class AccountController : Controller
	{
		public const string ADMIN = "Admin";
		public const string CLIENT = "Client";
		
		private readonly Dictionary<string, int> _roles = new Dictionary<string, int>
		{
			{ ADMIN, 0 },
			{ CLIENT, 1 }
		};
		
		private readonly IAccountRepository _accountRepository;
		
		public AccountController(IAccountRepository account_repository)
		{
			_accountRepository = account_repository;
		}
		
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _accountRepository.GetUser(model);
				if (user != null)
				{
					var role = _roles.FirstOrDefault(x => x.Value == user.Role).Key;
					await Authenticate(model.Email, role); // аутентификация

					return role == ADMIN
						? RedirectToAction("Index", "Admin")
						: RedirectToAction("Index", "Client");
				}
				
				ModelState.AddModelError("", "The user name or password provided is incorrect.");
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
				var user = await _accountRepository.GetUserByEmail(model.Email);
				if (user == null)
				{
					// добавляем пользователя в бд
					await _accountRepository.AddUser(model);
 
					await Authenticate(model.Email, CLIENT); // аутентификация
 
					return RedirectToAction("Index", "Client");
				}
				
				ModelState.AddModelError("Email", "Пользователь уже существует");
			}
			return View(model);
		}
 
		private async Task Authenticate(string email, string role)
		{
			// создаем один claim
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, email),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
			};
			// создаем объект ClaimsIdentity
			var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			// установка аутентификационных куки
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		}
 
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Account");
		}
	}
}