using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Billing.Models.DataModel;
using Billing.Models.Repository;
using Billing.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Billing.Controllers
{
	public class AccountController : Controller
	{
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
				User user = await _accountRepository.GetUser(model);
				if (user != null)
				{
					await Authenticate(model.Email); // аутентификация
 
					return RedirectToAction("Index", "Admin");
				}
				
				ModelState.AddModelError("", "The user name or password provided is incorrect.");
			}
			return View(model);
		}
		
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				User user = await _accountRepository.GetUserByEmail(model.Email);
				if (user == null)
				{
					// добавляем пользователя в бд
					await _accountRepository.AddUser(model);
 
					await Authenticate(model.Email); // аутентификация
 
					return RedirectToAction("Index", "Admin");
				}
				
				ModelState.AddModelError("Email", "Пользователь уже существует");
			}
			return View(model);
		}
 
		private async Task Authenticate(string userName)
		{
			// создаем один claim
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
			};
			// создаем объект ClaimsIdentity
			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
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