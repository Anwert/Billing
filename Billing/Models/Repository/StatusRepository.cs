using System;
using System.Threading.Tasks;
using Billing.Models.DataModel;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Billing.Models.Repository
{
	public class StatusRepository : BaseRepository, IStatusRepository
	{
		public StatusRepository(IConfiguration config) : base(config) { }

		public async Task<Status> GetStatusById(int id)
		{
			using (var conn = Connection)
			{
				conn.Open();
				return await conn.QuerySingleOrDefaultAsync<Status>($@"
select	status	{nameof(Status.Id)},
		name	{nameof(Status.Name)}
from	status
where	status = @{nameof(id)}
", new { id });
			}
		}
	}
}