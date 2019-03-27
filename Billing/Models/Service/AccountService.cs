using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Billing.Models.DataModel;
using Billing.Models.Repository;
using Billing.Models.ViewModels;

namespace Billing.Models.Service
{
	public class AccountService : IAccountService
	{
		public const string MANAGER = "Manager";
		public const string CLIENT = "Client";
		
		private readonly IAccountRepository _accountRepository;
		
		public AccountService(IAccountRepository account_repository)
		{
			_accountRepository = account_repository;
		}
		
		public async Task<User> GetUser(LoginModel model)
		{
			return await _accountRepository.GetUser(model);
		}

		public async Task AddUser(RegisterModel model)
		{
			await _accountRepository.AddUser(model);
		}

		public async Task<User> GetUserByEmail(string email)
		{
			return await _accountRepository.GetUserByEmail(email);
		}

		public ClaimsPrincipal GetClaimsPrincipal(string email, string role)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, email),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
			};

			var claims_identity = new ClaimsIdentity(claims, "ApplicationCookie",
				ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

			return new ClaimsPrincipal(claims_identity);
		}
	}
}