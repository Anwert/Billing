using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Models.DataModel;
using Billing.Models.ViewModels;

namespace Billing.Models.Repository
{
	public interface IUserRepository
	{
		Task<int> Create(User user);

		Task Delete(int id);

		Task<IEnumerable<User>> Get();

		Task<User> GetUser(int id);

		Task Update(User user);

		Task<User> GetUserByEmail(string email);
	}
}