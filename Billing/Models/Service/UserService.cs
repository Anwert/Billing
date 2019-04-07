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

		public async Task<int> CreateUserWithUserModel(UserModel model, string role)
		{
			var user = GetUserByModel(model, role);

			return await _userRepository.Create(user);
		}

		public async Task<User> GetUserById(int id) => await _userRepository.GetUserById(id);

		public async Task<IEnumerable<User>> GetClients() => await _userRepository.GetUsersWithRole(CLIENT_ROLE);

		public async Task<IEnumerable<User>> GetManagers() => await _userRepository.GetUsersWithRole(MANAGER_ROLE);
		
		public async Task<User> GetUserByUserModel(UserModel model)
		{
			var user = await _userRepository.GetUserByName(model.Name);
			
			return user?.Password == model.Password ? user : null;
		}

		public async Task<User> GetUserByName(string name) => await _userRepository.GetUserByName(name);
		
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

		public async Task<int> CreateClientWithUserModel(UserModel model)
		{
			var client  = GetUserByModel(model);
			
			return await _userRepository.Create(client);
		}
		
		public async Task<UserModel> GetUserModelById(int id)
		{
			var client = await _userRepository.GetUserById(id);
			
			return client?.Role == CLIENT_ROLE
				? GetModelByUser(client)
				: null;
		}
		
		public async Task UpdateUserWithUserModel(UserModel model)
		{
			var client = GetUserByModel(model);
			
			await _userRepository.Update(client);
		}
		
		private User GetUserByModel(UserModel model)
		{
			return new User
			{
				Id			= model.Id,
				Name		= model.Name,
				Contacts	= model.Contacts,
				Password	= model.Password,
				Role		= CLIENT_ROLE
			};
		}
		
		private User GetUserByModel(UserModel model, string role)
		{
			return new User
			{
				Id			= model.Id,
				Name		= model.Name,
				Contacts	= model.Contacts,
				Password	= model.Password,
				Role		= role
			};
		}
		
		private UserModel GetModelByUser(User user)
		{
			return new UserModel
			{
				Id			= user.Id,
				Name		= user.Name,
				Contacts	= user.Contacts,
				Password	= user.Password
			};
		}
	}
}
