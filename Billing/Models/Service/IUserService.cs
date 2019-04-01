using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Billing.Models.DataModel;
using Billing.Models.ViewModel;

namespace Billing.Models.Service
{
	public interface IUserService
	{
		Task<int> CreateUserWithUserModel(UserModel model, string role);

		Task<User> GetUserById(int id);

		Task<IEnumerable<User>>GetClients();
		
		Task<User> GetUserByUserModel(UserModel model);
		
		Task<User> GetUserByName(string name);

		ClaimsPrincipal GetClaimsPrincipal(string email, string role);
		
		Task<int> CreateClientWithUserModel(UserModel model);
		
		Task<UserModel> GetUserModelById(int id);
		
		Task UpdateUserWithUserModel(UserModel model);
	}
}