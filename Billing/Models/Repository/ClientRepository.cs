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

		public async Task<int> Create(string name)
		{
			using (var conn = Connection)
			{
				conn.Open();
				return await conn.ExecuteScalarAsync<int>($@"
insert	into client (name)
values	({nameof(name)})

select	scope_identity()
", new { name });
			}
		}

		public async Task<IEnumerable<Client>> Get()
		{
			using (var conn = Connection)
			{
				conn.Open();
				return await conn.QueryAsync<Client>($@"
select	client	{nameof(Client.Id)},
		name	{nameof(Client.Name)}
from	client
");
			}
		}

		public async Task<IEnumerable<Client>> GetClient(int id)
		{
			using (var conn = Connection)
			{
				conn.Open();
				return await conn.QueryAsync<Client>($@"
select	client	{nameof(Client.Id)},
		name	{nameof(Client.Name)}
from	client
where	client = {nameof(id)}
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
set		name	= {nameof(Client.Name)}
where	client	= {nameof(Client.Id)}
", new { client });
			}
		}

		public async Task Delete(int id)
		{
			using (var conn = Connection)
			{
				conn.Open();
				await conn.ExecuteAsync($@"
delete	from client
where	client = {nameof(id)}
", new { id });
			}
		}
	}
}
