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
		name	{nameof(Favour.Name)}
from	favour
where	favour = @{nameof(id)}
", new { id });
			}
		}
	}
}