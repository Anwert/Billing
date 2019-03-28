using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Models.DataModel;

namespace Billing.Models.Repository
{
	public interface IUserRepository
	{
		Task<int> Create(User user);

		Task Delete(int id);

		Task<IEnumerable<User>> GetUsers();

		Task<User> GetUserById(int id);

		Task Update(User user);

		Task<User> GetUserByName(string name);
		
		Task<IEnumerable<User>> GetClients();
	}
}