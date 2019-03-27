using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Billing.Models.DataModel;
using Billing.Models.ViewModels;

namespace Billing.Models.Service
{
	public interface IUserService
	{
		Task<int> Create(User user);
		
		Task<int> Create(RegisterModel model, string role);

		Task Delete(int id);

		Task<IEnumerable<User>> Get();

		Task<User> GetUser(int id);

		Task Update(User user);

		Task<IEnumerable<User>>GetClients();
		
		Task<User> GetUser(LoginModel model);
		
		Task<User> GetUserByEmail(string email);

		ClaimsPrincipal GetClaimsPrincipal(string email, string role);
	}
}