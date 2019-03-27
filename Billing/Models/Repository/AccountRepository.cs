using System.Threading.Tasks;
using Billing.Models.DataModel;
using Dapper;
using Microsoft.Extensions.Configuration;
using Billing.Models.ViewModels;

namespace Billing.Models.Repository
{
	public class AccountRepository : BaseRepository, IAccountRepository
	{
		public AccountRepository(IConfiguration config) : base(config) {}
		
		public async Task<User> GetUser(LoginModel model)
		{
			using (var conn = Connection)
			{
				conn.Open();
				return await conn.QuerySingleOrDefaultAsync<User>($@"
select	[user]		{nameof(User.Id)},
		email		{nameof(User.Email)},
		password	{nameof(User.Password)},
		role		{nameof(User.Role)}
from	[user]
where	email		=	@{nameof(model.Email)}
	and	password	=	@{nameof(model.Password)}
", new { model.Email, model.Password });
			}
		}
		
		public async Task<User> GetUserByEmail(string email)
		{
			using (var conn = Connection)
			{
				conn.Open();
				return await conn.QuerySingleOrDefaultAsync<User>($@"
select	[user]		{nameof(User.Id)},
		email		{nameof(User.Email)},
		password	{nameof(User.Password)},
		role		{nameof(User.Role)}
from	[user]
where	email = @{nameof(email)}
", new { email });
			}
		}

		public async Task AddUser(RegisterModel model)
		{
			using (var conn = Connection)
			{
				conn.Open();
				await conn.ExecuteAsync($@"
insert [user] (email, password)
values (@{nameof(model.Email)}, @{nameof(model.Password)})
", new { model.Email, model.Password });
			}
		}
	}
}