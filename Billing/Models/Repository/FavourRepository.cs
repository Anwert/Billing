using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Models.DataModel;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Billing.Models.Repository
{
	public class FavourRepository : BaseRepository, IFavourRepository
	{
		public FavourRepository(IConfiguration config) : base(config) { }

		public async Task<Favour> GetFavourById(int id)
		{
			using (var conn = Connection)
			{
				conn.Open();
				return await conn.QuerySingleOrDefaultAsync<Favour>($@"
select	favour	{nameof(Favour.Id)},
		name	{nameof(Favour.Name)},
		cost	{nameof(Favour.Cost)}
from	favour
where	favour = @{nameof(id)}
", new { id });
			}
		}

		public async Task<IEnumerable<Favour>> GetFavours()
		{
			using (var conn = Connection)
			{
				conn.Open();
				return await conn.QueryAsync<Favour>($@"
select	favour	{nameof(Favour.Id)},
		name	{nameof(Favour.Name)},
		cost	{nameof(Favour.Cost)}
from	favour
");
			}
		}

		public async Task<Favour> GetFavourByName(string name)
		{
			using (var conn = Connection)
			{
				conn.Open();
				return await conn.QuerySingleOrDefaultAsync<Favour>($@"
select	favour	{nameof(Favour.Id)},
		name	{nameof(Favour.Name)},
		cost	{nameof(Favour.Cost)}
from	favour
where	name = @{nameof(name)}
", new { name });
			}		}

		public async Task Create(Favour favour)
		{
			using (var conn = Connection)
			{
				conn.Open();
				await conn.ExecuteAsync($@"
insert	favour (name, cost)
values	(@{nameof(Favour.Name)}, @{nameof(Favour.Cost)})
", favour);
			}
		}
		
		public async Task Update(Favour favour)
		{
			using (var conn = Connection)
			{
				conn.Open();
				await conn.ExecuteAsync($@"
update	favour
set		name	= @{nameof(Favour.Name)},
		cost	= @{nameof(Favour.Cost)}
where	favour	= @{nameof(Favour.Id)}
", favour);
			}
		}
		
		public async Task Delete(int id)
		{
			using (var conn = Connection)
			{
				conn.Open();
				await conn.ExecuteAsync($@"
delete	from favour
where	favour = @{nameof(id)}
", new { id });
			}
		}
	}
}