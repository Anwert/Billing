using Billing.Models.DataModel;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Billing.Models.Repository
{
	public class ClientRepository : BaseRepository, IClientRepository
	{
		public ClientRepository(IConfiguration config) : base(config) { }

		public async Task<int> Create(Client client)
		{
			using (var conn = Connection)
			{
				conn.Open();
				return await conn.ExecuteScalarAsync<int>($@"
insert	client (name, email, address, phone)
values	(@{nameof(Client.Name)}, @{nameof(Client.Email)}, @{nameof(Client.Address)}, @{nameof(Client.Phone)})

select	scope_identity()
", new { client.Name, client.Email, client.Address, client.Phone });
			}
		}

		public async Task<IEnumerable<Client>> Get()
		{
			using (var conn = Connection)
			{
				conn.Open();
				return await conn.QueryAsync<Client>($@"
select	client	{nameof(Client.Id)},
		name	{nameof(Client.Name)},
		email	{nameof(Client.Email)},
		address	{nameof(Client.Address)},
		phone	{nameof(Client.Phone)}
from	client
");
			}
		}

		public async Task<Client> GetClient(int id)
		{
			using (var conn = Connection)
			{
				conn.Open();
				return await conn.ExecuteScalarAsync<Client>($@"
select	client	{nameof(Client.Id)},
		name	{nameof(Client.Name)},
		email	{nameof(Client.Email)},
		address	{nameof(Client.Address)},
		phone	{nameof(Client.Phone)}
from	client
where	client = @{nameof(id)}
", new { id });
			}
		}

		public async Task Update(Client client)
		{
			using (var conn = Connection)
			{
				conn.Open();
				await conn.ExecuteAsync($@"
update	client
set		name	= {nameof(Client.Name)},
		email	= {nameof(Client.Email)},
		address	= {nameof(Client.Address)},
		phone	= {nameof(Client.Phone)}
where	client	= @{nameof(client.Id)}
", new { client.Name, client.Email, client.Address, client.Phone, client.Id });
			}
		}

		public async Task Delete(int id)
		{
			using (var conn = Connection)
			{
				conn.Open();
				await conn.ExecuteAsync($@"
delete	from client
where	client = @{nameof(id)}
", new { id });
			}
		}
	}
}
