using Billing.Models.DataModel;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Billing.Models.Repository
{
	public class UserRepository : BaseRepository, IUserRepository
	{
		public UserRepository(IConfiguration config) : base(config) { }

		public async Task<int> Create(User user)
		{
			using (var conn = Connection)
			{
				conn.Open();
				return await conn.ExecuteScalarAsync<int>($@"
insert	[user] (email, name, password, contacts, role)
values	(@{nameof(User.Email)}, @{nameof(User.Name)}, @{nameof(User.Password)}, @{nameof(User.Contacts)}, @{nameof(User.Role)})

select	scope_identity()
", new { user.Email, user.Name, user.Password, user.Contacts, user.Role });
			}
		}

		public async Task<IEnumerable<User>> Get()
		{
			using (var conn = Connection)
			{
				conn.Open();
				return await conn.QueryAsync<User>($@"
select	[user]		{nameof(User.Id)},
		email		{nameof(User.Email)},
		name		{nameof(User.Name)},
		password	{nameof(User.Password)},
		contacts	{nameof(User.Contacts)},
		role		{nameof(User.Role)}
from	[user]
");
			}
		}

		public async Task<User> GetUser(int id)
		{
			using (var conn = Connection)
			{
				conn.Open();
				return await conn.QuerySingleOrDefaultAsync<User>($@"
select	[user]		{nameof(User.Id)},
		email		{nameof(User.Email)},
		name		{nameof(User.Name)},
		password	{nameof(User.Password)},
		contacts	{nameof(User.Contacts)},
		role		{nameof(User.Role)}
from	[user]
where	[user] = @{nameof(id)}
", new { id });
			}
		}

		public async Task Update(User user)
		{
			using (var conn = Connection)
			{
				conn.Open();
				await conn.ExecuteAsync($@"
update	client
set		[user]		{nameof(User.Id)},
		email		{nameof(User.Email)},
		name		{nameof(User.Name)},
		password	{nameof(User.Password)},
		contacts	{nameof(User.Contacts)},
		role		{nameof(User.Role)}
where	[user] = @{nameof(user.Id)}
", new { user.Email, user.Name, user.Password, user.Contacts, user.Role });
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
		name		{nameof(User.Name)},
		password	{nameof(User.Password)},
		contacts	{nameof(User.Contacts)},
		role		{nameof(User.Role)}
from	[user]
where	email = @{nameof(email)}
", new {email});
			}
		}

		public async Task Delete(int id)
		{
			using (var conn = Connection)
			{
				conn.Open();
				await conn.ExecuteAsync($@"
delete	from [user]
where	[user] = @{nameof(id)}
", new { id });
			}
		}
	}
}
