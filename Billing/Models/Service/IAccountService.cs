using System.Security.Claims;
using System.Threading.Tasks;
using Billing.Models.DataModel;
using Billing.Models.ViewModels;

namespace Billing.Models.Service
{
	public interface IAccountService
	{
		Task<User> GetUser(LoginModel model);

		Task AddUser(RegisterModel model);

		Task<User> GetUserByEmail(string email);
		
		ClaimsPrincipal GetClaimsPrincipal(string email, string role);
	}
}