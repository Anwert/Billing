using Billing.Models.DataModel;
using Billing.Models.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Billing.Models.Service
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository user_repository)
		{
			_userRepository = user_repository;
		}

		public async Task<int> Create(User user)
		{
			return await _userRepository.Create(user);
		}

		public async Task Delete(int id)
		{
			await _userRepository.Delete(id);
		}

		public async Task<IEnumerable<User>> GetClients()
		{
			return await _userRepository.GetClients();
		}

		public async Task<User> GetClient(int id)
		{
			return await _userRepository.GetClient(id);
		}

		public async Task Update(User user)
		{
			await _userRepository.Update(user);
		}
	}
}
