using Billing.Models.DataModel;
using Billing.Models.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Billing.Models.ViewModels;

namespace Billing.Models.Service
{
	public class UserService : IUserService
	{
		public const string MANAGER_ROLE = "Manager";
		public const string CLIENT_ROLE = "Client";
		
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository user_repository)
		{
			_userRepository = user_repository;
		}

		public async Task<int> Create(User user)
		{
			return await _userRepository.Create(user);
		}

		public async Task<int> Create(RegisterModel model, string role)
		{
			var user = new User
			{
				Email = model.Email,
				Name = model.Name,
				Contacts = model.Contacts,
				Password = model.Password,
				Role = role
			};

			return await Create(user);
		}

		public async Task Delete(int id)
		{
			await _userRepository.Delete(id);
		}

		public async Task<IEnumerable<User>> Get()
		{
			return await _userRepository.Get();
		}

		public async Task<User> GetUser(int id)
		{
			return await _userRepository.GetUser(id);
		}

		public async Task Update(User user)
		{
			await _userRepository.Update(user);
		}

		public async Task<IEnumerable<User>> GetClients()
		{
			var users = await Get();
			return users.Where(x => x.Role == CLIENT_ROLE);
		}

		public async Task<User> GetUser(LoginModel model)
		{
			var user = await GetUserByEmail(model.Email);
			
			return user?.Password == model.Password ? user : null;
		}

		public async Task<User> GetUserByEmail(string email)
		{
			return await _userRepository.GetUserByEmail(email);
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
