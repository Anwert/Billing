using System.Threading.Tasks;
using Billing.Models.DataModel;
using Billing.Models.ViewModels;

namespace Billing.Models.Repository
{
	public interface IAccountRepository
	{
		Task<User> GetUser(LoginModel model);

		Task AddUser(RegisterModel model);

		Task<User> GetUserByEmail(string email);
	}
}