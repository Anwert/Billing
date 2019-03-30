using Billing.Models.DataModel;
using Billing.Models.Repository;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Billing.Models.ViewModel;

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
				Name		= model.Name,
				Contacts	= model.Contacts,
				Password	= model.Password,
				Role		= role
			};

			return await _userRepository.Create(user);
		}

		public async Task Delete(int id)
		{
			await _userRepository.Delete(id);
		}

		public async Task<IEnumerable<User>> GetUsers()
		{
			return await _userRepository.GetUsers();
		}

		public async Task<User> GetUserById(int id)
		{
			return await _userRepository.GetUserById(id);
		}

		public async Task Update(User user)
		{
			await _userRepository.Update(user);
		}

		public async Task<IEnumerable<User>> GetClients()
		{
			return await _userRepository.GetClients();
		}

		public async Task<User> GetUserByLoginModel(LoginModel model)
		{
			var user = await _userRepository.GetUserByName(model.Name);
			
			return user?.Password == model.Password ? user : null;
		}

		public async Task<User> GetUserByName(string name)
		{
			return await _userRepository.GetUserByName(name);
		}
		
		public ClaimsPrincipal GetClaimsPrincipal(string name, string role)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, name),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
			};

			var claims_identity = new ClaimsIdentity(claims, "ApplicationCookie",
				ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			
			return new ClaimsPrincipal(claims_identity);
		}
	}
}
