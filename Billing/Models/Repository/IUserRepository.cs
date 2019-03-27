using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Models.DataModel;

namespace Billing.Models.Repository
{
	public interface IUserRepository
	{
		Task<int> Create(User user);

		Task Delete(int id);

		Task<IEnumerable<User>> GetClients();

		Task<User> GetClient(int id);

		Task Update(User user);
	}
}